using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Edge {
	public Point A, B;
	public Vector3d p, n;

	List<Cell> cells;

	public static Edge GetEdge(Point A, Point B, IIsoSurface surface, List<Edge> edges)
	{
		Vector3d p;
		
		if(A.iso == 0)
		{
			p = A.p;
		}
		else if(B.iso == 0)
		{
			p = B.p;
		}
		else
		{
			Vector3d diff = B.p - A.p;
			Vector3d start = A.p;
			
			double ratio = 0.5f;
			p = start + (diff * ratio);
			
			double f = 0.25f;
			double iso;
			while((iso = surface.sample(p.x,p.y,p.z)) > 0.0001)
			{
				ratio += f * Math.Sign(iso * A.iso);
				f /= 2;
				p = start + (diff * ratio);
			}
		}

		Vector3d n = surface.sampleDerivative(p.x,p.y,p.z);
		//Debug.DrawLine(A.p, B.p, new Color(0,0,1,0.5f), 30);
		return new Edge(p,n,A,B,edges);
	}

	public Edge(Vector3d p, Vector3d n, Point A, Point B, List<Edge> edges){
		this.p = p;
		this.n = n;
		this.A = A;
		this.B = B;

		cells = new List<Cell>();

		A.edges.Add(this);
		B.edges.Add(this);
		
		edges.Add(this);
	}

	public void DebugDraw(double cellSize)
	{
		Debug.DrawLine((Vector3)A.p, (Vector3)B.p, new Color(0,0,0,0.5f));
		Debug.DrawRay((Vector3)p,(Vector3)n,Color.red);
	}

	public void addCell(Cell cell)
	{
		cells.Add(cell);
	}

	public int Draw(List<Vector3> vertices, List<Vector3> normals, List<int> triangles, IIsoSurface surface)
	{
		if(this.cells.Count < 4) return 0;

		int start = vertices.Count;

		int i = 0;
		foreach(Cell cell in cells)
		{
			i++;
			Vector3d p = cell.getPoint();
			vertices.Add((Vector3)p);
			normals.Add((Vector3)surface.sampleDerivative(p.x, p.y, p.z));
		}

		List<int> tris = new List<int>();

		tris.Add(start);
		tris.Add(start + 1);
		tris.Add(start + 2);

		tris.Add(start + 1);
		tris.Add(start + 3);
		tris.Add(start + 2);
		
		/*for(int j = 0; j < tris.Count; j++)
		{
			triangles.Add(tris[j]);
		}*/

		Vector3d diff = B.p - A.p;

		for(int j = 0; j < tris.Count; j++)
		{
			triangles.Add(tris[j]);
		}
		for(int j = tris.Count - 1; j > -1; j--)
		{
			triangles.Add(tris[j]);
		}

		return i;
	}
}
