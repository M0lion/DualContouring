using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public class PlaneSurface : IIsoSurface
{
    public double sample(Vector<double> pos)
    {
        return (pos[0] + pos[1] + pos[2]) - 12.5f;
    }

    public Vector<double> sampleDerivative(Vector<double> pos)
    {
		return Vector<double>.Build.DenseOfArray(new double[] {1,1,1});
    }
}
