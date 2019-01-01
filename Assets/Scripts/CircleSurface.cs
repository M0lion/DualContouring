using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public class CircleSurface : IsoSurface {
	double radius;
	Vector<double> pos;

	public CircleSurface(double r, Vector<double> pos, double delta)
	{
		radius = r;
		this.delta = delta;
		this.pos = pos;
	}

	public override double sample(Vector<double> pos)
	{		
		return (pos - this.pos).L2Norm() - radius;
	}
}
