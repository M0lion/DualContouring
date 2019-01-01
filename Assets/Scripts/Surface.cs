using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

public class Surface : IIsoSurface
{
	double pos;

	public Surface(double pos)
	{
		this.pos = pos;
	}

    public double sample(Vector<double> pos)
    {
        return pos[1] - this.pos - Math.Sin(pos[0]) - Math.Sin(pos[2]);
    }

    public Vector<double> sampleDerivative(Vector<double> pos)
    {
        return Vector<double>.Build.DenseOfArray(new double[] {-Math.Cos(pos[0]),1,-Math.Cos(pos[2])});
    }
}
