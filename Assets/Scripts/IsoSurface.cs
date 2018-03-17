using UnityEngine;

public  interface IIsoSurface
{
	float sample(float x, float y, float z);
	Vector3 sampleDerivative(float x, float y, float z);
}

public abstract class IsoSurface : IIsoSurface
{
	abstract public float sample(float x, float y, float z);

	protected float delta;

	public Vector3 sampleDerivative(float x, float y, float z) 
	{
		float ddelta = delta / 2;
		float xx = sample(x + ddelta,y,z) - sample(x - ddelta, y,z);
		float yy = sample(x,y + ddelta,z) - sample(x, y - ddelta,z);
		float zz = sample(x,y,z + ddelta) - sample(x, y,z - ddelta);

		return new Vector3(xx,yy,zz)/delta;
	}
}