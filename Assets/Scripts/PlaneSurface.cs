using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSurface : IIsoSurface
{
    public double sample(double x, double y, double z)
    {
        return (x + y + z) - 12.5f;
    }

    public Vector3d sampleDerivative(double x, double y, double z)
    {
		return new Vector3d(1,1,1);
    }
}
