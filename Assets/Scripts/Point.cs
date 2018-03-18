using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Point {
	public double iso;

	public Vector3d p;

	public List<Edge> edges;

	int x,y,z;

	public Point(double iso, Vector3d p, int x, int y, int z)
	{
		this.iso = iso;
		edges = new List<Edge>();
		this.p = p;
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Edge getEdge(Point point, IIsoSurface surface, List<Edge> edges)
	{
		//Debug.DrawLine((Vector3)p, (Vector3)point.p, new Color(0.2f,0.2f,0.2f,0.2f), 30);
		foreach(Edge edge in this.edges)
		{
			if(edge.A == point || edge.B == point)
			{
				//Debug.DrawLine((Vector3)p, (Vector3)point.p, new Color(1,0,0,0.4f), 30);
				return edge;
			}
		}

		Edge e = Edge.GetEdge(this, point, surface, edges);
		//Debug.DrawLine((Vector3)p, (Vector3)point.p, new Color(0,0,1,0.4f), 30);
		return e;
	}

	public void DebugDraw(){
		double red = Math.Max(iso*10,0);
		double blue = Math.Max(-iso*10,0);

		Debug.DrawLine((Vector3)(p + new Vector3d(0.1f,0,0)), (Vector3)(p + new Vector3d(-0.1f,0,0)), new Color((float)red, 0, (float)blue, 0.2f));
		Debug.DrawLine((Vector3)(p + new Vector3d(0,0.1f,0)), (Vector3)(p + new Vector3d(0,-0.1f,0)), new Color((float)red, 0, (float)blue, 0.2f));
		Debug.DrawLine((Vector3)(p + new Vector3d(0,0,0.1f)), (Vector3)(p + new Vector3d(0,0,-0.1f)), new Color((float)red, 0, (float)blue, 0.2f));
	}
}
