using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

public class CellTest : MonoBehaviour {

	Cell cell;

	public int maxLevel = 5;
	public double size = 5;
	IsoSurface surface;

	void Start () {
		var cellPos = Vector<double>.Build.DenseOfArray(new double [] {0,0,0});
		var circlePos = Vector<double>.Build.DenseOfArray(new double [] {2.5,2.5,2.5});
		surface = new CircleSurface(2.4, circlePos, 0.01);
		cell = Cell.Create(cellPos, size, surface, maxLevel);
		cell.EnumerateEdges((Cell[] edge, int dimensionMap) => {
			var a = edge[0].pointIndexToWorldPos(dimensionMap);
			var b = edge[0].pointIndexToWorldPos((int)Math.Pow(2,3) - 1);

			var point = getEdgePoint(a,b,edge[0].getPoint(dimensionMap),edge[0].getPoint((int)Math.Pow(2,3) - 1));
			var n = surface.sampleDerivative(point);

			Debug.DrawRay(MakeUnityVector.GetVector(point), MakeUnityVector.GetVector(n), Color.red, 1000);
			Debug.DrawLine(MakeUnityVector.GetVector(a),MakeUnityVector.GetVector(b), Color.white, 1000);

			foreach(Cell cell in edge) {
				//cell.addEdge(point, n);
			}
		});
		/* cell.ForEach((Cell Cell) => {
			cell.SolveQEF();
		}); */
		/* cell.EnumerateEdges((Cell[] edge, int dimensionMap) => {
			for(int i = 0; i < edge.Length; i++) {
				var a = edge[i].Point;
				var b = edge[(i + 1)%4].Point;

				if(a == null || b == null) continue;

				var A = MakeUnityVector.GetVector(a);
				var B = MakeUnityVector.GetVector(b);

				//Debug.DrawLine(A, B, Color.black, 1000);
			}
		}); */
	}

	void Update () {
		//DrawCells();
	}

	Vector<double> getEdgePoint(Vector<double> a, Vector<double> b, double valA, double valB) {
		double sample;
		Vector<double> point;
		do {
			point = (a + b) / 2.0;
			sample = surface.sample(point);
			if(Math.Sign(sample) == Math.Sign(valA)) {
				a = point;
				valA = sample;
			} else {
				b = point;
				valB = sample;
			}
		} while(sample < 0.02);

		return point;
	}

	void DrawCells() {
		cell.ForEach(cell => {
			var color = cell.level / (float)maxLevel;
			var Color = new Color(1 - color,1 - color,1 - color, color);

			switch(cell.level) {
				case 1: Color = Color.red; break;
				case 2: Color = Color.green; break;
				case 3: Color = Color.blue; break;
				case 4: Color = Color.magenta; break;
				case 5: Color = Color.cyan; break;
				case 6: Color = Color.yellow; break;
			}

			Color.a = 0.8f;

			//Debug.DrawLine(Vector3.zero, MakeUnityVector.GetVector(cell.pointIndexToWorldPos(0)), Color);
					
			for(int a = 0; a < Math.Pow(2, cell.Dimensions); a++) {
				for(int b = 0; b < Math.Pow(2, cell.Dimensions); b++) {
					if(a == b) continue;

					var c = a ^ b;
					var sum = 0;
					foreach(bool axis in cell.pointIndexToPos(c)) {
						if(axis) sum++;
					}
					if(sum > 1) continue;

					var A = MakeUnityVector.GetVector(cell.pointIndexToWorldPos(a));
					var B = MakeUnityVector.GetVector(cell.pointIndexToWorldPos(b));

					Debug.DrawLine(A,B,Color);
				}
			}	
		});
	}
}
