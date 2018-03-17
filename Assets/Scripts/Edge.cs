using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge {
	public Point A, B;
	Vector3 p, n;

	public static Edge GetEdge(Point A, Point B, IIsoSurface surface, List<Edge> edges)
	{
		Vector3 p = B.p - A.p;
		p = A.p + (p * (B.iso / (A.iso + B.iso)));
		//TODO: position p better
		Vector3 n = surface.sampleDerivative(p.x,p.y,p.z);
		return new Edge(p,n,A,B,edges);
	}

	public Edge(Vector3 p, Vector3 n, Point A, Point B, List<Edge> edges){
		this.p = p;
		this.n = n;
		this.A = A;
		this.B = B;

		A.edges.Add(this);
		B.edges.Add(this);
		
		edges.Add(this);
	}

	public void DebugDraw()
	{
		Debug.DrawLine(A.p, B.p, Color.gray);
		Debug.DrawRay(p,n,Color.red);
	}
}
