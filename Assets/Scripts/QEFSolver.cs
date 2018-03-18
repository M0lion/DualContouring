using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class QEFSolver {
	public static double Error(Vector3d[] p, Vector3d[] n, Vector3d x)
	{
		if(p.Length != n.Length) throw new UnityException("p and b not of equal length");

		double error = 0;
		for(int i = 0; i < p.Length; i++)
		{
			error += Math.Pow(Vector3d.Dot(n[i], x - p[i]) ,2);
		}

		return error;
	}

	public static Vector3d minimize(Vector3d[] p, Vector3d[] n, Vector3d start, int iterations, double delta)
	{
		double d = delta / 2;
		Vector3d point = new Vector3d(start.x, start.y, start.z);
		for(int i = 0; i < iterations; i++)
		{
			double x = Error(p,n,new Vector3d(point.x - d, point.y, point.z)) - Error(p,n,new Vector3d(point.x + d, point.y, point.z));
			double y = Error(p,n,new Vector3d(point.x, point.y - d, point.z)) - Error(p,n,new Vector3d(point.x, point.y + d, point.z));
			double z = Error(p,n,new Vector3d(point.x, point.y, point.z - d)) - Error(p,n,new Vector3d(point.x, point.y, point.z + d));

			Vector3d der = new Vector3d(x,y,z) / delta;
			double error = Math.Sqrt(Error(p,n,point));
			point += der * error / 25;
		}

		Debug.Log("Error: " + Error(p,n,point) + " - " + p.Length);
		return point;
	}
}
