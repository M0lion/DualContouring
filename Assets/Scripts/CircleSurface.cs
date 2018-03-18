using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSurface : IsoSurface {
	float radius;
	Vector3 pos;

	public CircleSurface(float r, Vector3 pos, float delta)
	{
		radius = r;
		this.delta = delta;
		this.pos = pos;
	}

	public override float sample(float x, float y, float z)
	{
		Vector3 vec = new Vector3(x,y,z);
		
		return (vec - pos).magnitude - radius;
	}
}
