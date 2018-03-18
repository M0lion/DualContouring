using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSurface : IIsoSurface
{
    public float sample(float x, float y, float z)
    {
        return (x + y + z) - 12.5f;
    }

    public Vector3 sampleDerivative(float x, float y, float z)
    {
		return new Vector3(1,1,1);
    }
}
