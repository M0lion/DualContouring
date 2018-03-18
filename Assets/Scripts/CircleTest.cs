using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CircleTest : MonoBehaviour {

	MeshFilter meshFilter;

	List<Edge> edges;
	Point[,,] points;
	Cell[,,] cells;

	public float cellSize = 1f;
	public int size = 10;
	public bool debug;

	IIsoSurface surface;

	// Use this for initialization
	void Start () {

		//surface = new CircleSurface(4.25f, new Vector3(5,5,5), 0.0001f);
		surface = new Surface(5.4f);
		
		edges = new List<Edge>();
		points = new Point[size + 1,size + 1,size + 1];
		cells = new Cell[size,size,size];

		for(int x = 0; x < size; x++)
		{
			for(int y = 0; y < 10; y++)
			{
				for(int z = 0; z < size; z++)
				{
					cells[x,y,z] = new Cell(x,y,z,cellSize, points, edges, surface);
				}	
			}
		}

		meshFilter = GetComponent<MeshFilter>();

		Mesh mesh = new Mesh();
		
		List<Vector3> vertices = new List<Vector3>();
		List<Vector3> normals = new List<Vector3>();
		List<int> triangles = new List<int>();

		foreach(Edge edge in edges)
		{
			int i = edge.Draw(vertices, normals, triangles, surface);
			Debug.Log("I: " + i);
		}

		mesh.vertices = vertices.ToArray();
		mesh.normals = normals.ToArray();
		mesh.triangles = triangles.ToArray();

		meshFilter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		if(debug)
		{
			foreach(Point point in points)
			{
				point.DebugDraw();
			}
			foreach(Cell cell in cells)
			{
				cell.DebugDraw(surface);
			}
			foreach(Edge edge in edges)
			{
				edge.DebugDraw(cellSize);
			}
		}
	}
}
