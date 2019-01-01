using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public  interface IIsoSurface
{
	double sample(Vector<double> pos);
	Vector<double> sampleDerivative(Vector<double> pos);
}

public abstract class IsoSurface : IIsoSurface
{
	abstract public double sample(Vector<double> pos);

	protected double delta;

	public Vector<double> sampleDerivative(Vector<double> pos) 
	{		
		Vector<double> result = Vector<double>.Build.Dense(pos.Count);

		for(var i = 0; i < pos.Count; i++) {
			var d = Vector<double>.Build.Dense(pos.Count, 0);
			d[i] = delta / 2;
			result[i] = sample(pos + d) - sample(pos - d);
		}

		return result / delta;
	}
}