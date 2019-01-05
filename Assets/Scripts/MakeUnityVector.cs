using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

abstract class MakeUnityVector
{
    public static Vector3 GetVector(Vector<double> vector) {
        if(vector.Count < 3) throw new System.Exception("vector needs at lest 3 dimensions");
        var newVector = Vector3.zero;
        newVector.x = (float)vector[0];
        newVector.y = (float)vector[1];
        newVector.z = (float)vector[2];
        return newVector;
    }
}