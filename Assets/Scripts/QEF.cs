using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Collections.Generic;

public class QEF {

    Matrix<double> AtA;
    Vector<double> Atb;

    public Vector<double> solution;
    public double error;

    public QEF(List<Vector<double>> pi, List<Vector<double>> ni) {
        Matrix<double> A = Matrix.Build.Dense(pi.Count, 3);
        for(var i = 0; i < ni.Count; i++) {
            A.SetRow(i, ni[i]);
        }

        Vector<double> b = Vector.Build.Dense(ni.Count);
        for(var i = 0; i < ni.Count; i++) {
            b[i] = ni[i].DotProduct(pi[i]);
        }

        Matrix<double> At = A.Transpose();

        AtA = At.Multiply(A);
        Atb = At.Multiply(b);

        solution = AtA.Solve(Atb);

        error = 0;

        for(var i = 0; i < pi.Count; i++) {
            error += System.Math.Abs((solution - pi[i]).DotProduct(ni[i]));
        }
    }
}