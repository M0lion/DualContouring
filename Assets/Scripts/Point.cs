using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
	public float iso;

	public Vector3 p;

	public List<Edge> edges;

	public Point(float iso, Vector3 p)
	{
		this.iso = iso;
		edges = new List<Edge>();
	}

	public Edge getEdge(Point point, IIsoSurface surface, List<Edge> edges)
	{
		foreach(Edge edge in edges)
		{
			if(edge.A == point || edge.B == point)
				return edge;
		}

		return Edge.GetEdge(this, point, surface, edges);
	}
}
