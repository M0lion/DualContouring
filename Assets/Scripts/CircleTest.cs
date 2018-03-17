using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour {

	List<Edge> edges;
	Point[,,] points;
	Cell[,,] cells;

	float cellSize = 1;

	IIsoSurface surface;

	// Use this for initialization
	void Start () {
		surface = new CircleSurface(10,0.002f);
		
		edges = new List<Edge>();
		points = new Point[11,11,11];
		cells = new Cell[10,10,10];

		for(int x = 0; x < 10; x++)
		{
			for(int y = 0; y < 10; y++)
			{
				for(int z = 0; z < 10; z++)
				{
					cells[x,y,z] = new Cell(x,y,z,cellSize, points, edges, surface);
				}	
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Edge edge in edges)
		{
			edge.DebugDraw();
		}
	}
}
