using UnityEngine;

public  interface IIsoSurface
{
	double sample(double x, double y, double z);
	Vector3d sampleDerivative(double x, double y, double z);
}

public abstract class IsoSurface : IIsoSurface
{
	abstract public double sample(double x, double y, double z);

	protected double delta;

	public Vector3d sampleDerivative(double x, double y, double z) 
	{
		double ddelta = delta / 2;
		double xx = sample(x + ddelta,y,z) - sample(x - ddelta, y,z);
		double yy = sample(x,y + ddelta,z) - sample(x, y - ddelta,z);
		double zz = sample(x,y,z + ddelta) - sample(x, y,z - ddelta);

		return new Vector3d(xx,yy,zz)/delta;
	}
}