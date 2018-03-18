using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge {
	public Point A, B;
	public Vector3 p, n;

	List<Cell> cells;

	public static Edge GetEdge(Point A, Point B, IIsoSurface surface, List<Edge> edges)
	{
		Vector3 p;
		
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
			Vector3 diff = B.p - A.p;
			Vector3 start = A.p;
			
			float ratio = 0.5f;
			p = start + (diff * ratio);
			
			float f = 0.25f;
			float iso;
			while((iso = surface.sample(p.x,p.y,p.z)) > 0.0001)
			{
				ratio += f * Mathf.Sign(iso * A.iso);
				f /= 2;
				p = start + (diff * ratio);
			}
		}

		Vector3 n = surface.sampleDerivative(p.x,p.y,p.z);
		Debug.DrawLine(A.p, B.p, new Color(0,0,1,0.5f), 30);
		return new Edge(p,n,A,B,edges);
	}

	public Edge(Vector3 p, Vector3 n, Point A, Point B, List<Edge> edges){
		this.p = p;
		this.n = n;
		this.A = A;
		this.B = B;

		cells = new List<Cell>();

		A.edges.Add(this);
		B.edges.Add(this);
		
		edges.Add(this);
	}

	public void DebugDraw(float cellSize)
	{
		Debug.DrawLine(A.p, B.p, new Color(0,0,0,0.5f));
		Debug.DrawRay(p,n,Color.red);
	}

	public void addCell(Cell cell)
	{
		cells.Add(cell);
	}

	public int Draw(List<Vector3> vertices, List<Vector3> normals, List<int> triangles, IIsoSurface surface)
	{
		int start = vertices.Count;

		int i = 0;
		foreach(Cell cell in cells)
		{
			i++;
			Vector3 p = cell.getPoint();
			vertices.Add(p);
			normals.Add(surface.sampleDerivative(p.x, p.y, p.z));
		}

		List<int> tris = new List<int>();

		tris.Add(start);
		tris.Add(start + 1);
		tris.Add(start + 3);
		
		if(A.iso < 0)
		{
			for(int j = 0; j < tris.Count; j++)
			{
				triangles.Add(tris[j]);
			}
		}
		else
		{
			for(int j = tris.Count - 1; j > -1; j--)
			{
				triangles.Add(tris[j]);
			}
		}

		/*triangles.Add(start + 2);
		triangles.Add(start + 3);
		triangles.Add(start + 1);
		triangles.Add(start + 3);
		triangles.Add(start + 1);
		triangles.Add(start);*/

		return i;
	}
}
