using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : IIsoSurface
{
	float pos;

	public Surface(float pos)
	{
		this.pos = pos;
	}

    public float sample(float x, float y, float z)
    {
        return y - pos - Mathf.Sin(x) - Mathf.Sin(z);
    }

    public Vector3 sampleDerivative(float x, float y, float z)
    {
        return new Vector3(-Mathf.Cos(x),1,-Mathf.Cos(z));
    }
}
