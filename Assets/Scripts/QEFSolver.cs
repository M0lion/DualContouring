using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QEFSolver {
	public static float Error(Vector3[] p, Vector3[] n, Vector3 x)
	{
		if(p.Length != n.Length) throw new UnityException("p and b not of equal length");

		float error = 0;
		for(int i = 0; i < p.Length; i++)
		{
			error += Mathf.Pow( Vector3.Dot(n[i], x - p[i]) ,2);
		}

		return error;
	}

	public static Vector3 minimize(Vector3[] p, Vector3[] n, Vector3 start, int iterations, float delta)
	{
		float d = delta / 2;
		Vector3 point = new Vector3(start.x, start.y, start.z);
		for(int i = 0; i < iterations; i++)
		{
			float x = Error(p,n,new Vector3(point.x - d, point.y, point.z)) - Error(p,n,new Vector3(point.x + d, point.y, point.z));
			float y = Error(p,n,new Vector3(point.x, point.y - d, point.z)) - Error(p,n,new Vector3(point.x, point.y + d, point.z));
			float z = Error(p,n,new Vector3(point.x, point.y, point.z - d)) - Error(p,n,new Vector3(point.x, point.y, point.z + d));

			Vector3 der = new Vector3(x,y,z) / delta;
			float error = Mathf.Sqrt(Error(p,n,point));
			point += der * error / 25;
		}

		Debug.Log("Error: " + Error(p,n,point) + " - " + p.Length);
		return point;
	}
}
