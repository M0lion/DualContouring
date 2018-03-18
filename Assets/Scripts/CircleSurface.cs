using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSurface : IsoSurface {
	double radius;
	Vector3d pos;

	public CircleSurface(double r, Vector3d pos, double delta)
	{
		radius = r;
		this.delta = delta;
		this.pos = pos;
	}

	public override double sample(double x, double y, double z)
	{
		Vector3d vec = new Vector3d(x,y,z);
		
		return (vec - pos).magnitude - radius;
	}
}
