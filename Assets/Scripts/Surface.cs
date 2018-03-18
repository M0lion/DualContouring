using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Surface : IIsoSurface
{
	double pos;

	public Surface(double pos)
	{
		this.pos = pos;
	}

    public double sample(double x, double y, double z)
    {
        return y - pos - Math.Sin(x) - Math.Sin(z);
    }

    public Vector3d sampleDerivative(double x, double y, double z)
    {
        return new Vector3d(-Math.Cos(x),1,-Math.Cos(z));
    }
}
