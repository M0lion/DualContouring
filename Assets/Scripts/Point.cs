using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
	public float iso;

	public Vector3 p;

	public List<Edge> edges;

	int x,y,z;

	public Point(float iso, Vector3 p, int x, int y, int z)
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
		Debug.DrawLine(p, point.p, new Color(0.2f,0.2f,0.2f,0.2f), 30);
		foreach(Edge edge in this.edges)
		{
			if(edge.A == point || edge.B == point)
			{
				Debug.DrawLine(p, point.p, new Color(1,0,0,0.4f), 30);
				return edge;
			}
		}

		Edge e = Edge.GetEdge(this, point, surface, edges);
		Debug.DrawLine(p, point.p, new Color(0,0,1,0.4f), 30);
		return e;
	}

	public void DebugDraw(){
		float red = Mathf.Clamp(iso*10,0,1);
		float blue = Mathf.Clamp(-iso*10,0,1);

		Debug.DrawLine(p + new Vector3(0.1f,0,0), p + new Vector3(-0.1f,0,0), new Color(red, 0, blue, 0.2f));
		Debug.DrawLine(p + new Vector3(0,0.1f,0), p + new Vector3(0,-0.1f,0), new Color(red, 0, blue, 0.2f));
		Debug.DrawLine(p + new Vector3(0,0,0.1f), p + new Vector3(0,0,-0.1f), new Color(red, 0, blue, 0.2f));
	}
}
