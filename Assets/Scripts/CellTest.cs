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

	void Start () {
		var cellPos = Vector<double>.Build.DenseOfArray(new double [] {0,0,0});
		var circlePos = Vector<double>.Build.DenseOfArray(new double [] {2.5,2.5,2.5});
		var surface = new CircleSurface(2.4, circlePos, 0.01);
		cell = Cell.Create(cellPos, size, surface, maxLevel);
	}

	void Update () {
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
