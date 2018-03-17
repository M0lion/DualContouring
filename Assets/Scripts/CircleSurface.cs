using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSurface : IsoSurface {
	float radius;

	public CircleSurface(float r, float delta)
	{
		radius = r;
		this.delta = delta;
	}

	public override float sample(float x, float y, float z)
	{
		Vector3 vec = new Vector3(x,y,z);
		
		return vec.magnitude - radius;
	}
}
