using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {
	Point[,,] points;
	List<Edge> edges;

	public Cell(int x, int y, int z, float cellSize, Point[,,] points, List<Edge> edges, IIsoSurface surface)
	{
		this.points = new Point[2,2,2];
		for(int xx = 0; xx < 2; xx++)
		{
			for(int yy = 0; yy < 2; yy++)
			{
				for(int zz = 0; zz < 2; zz++)
				{
					this.points[xx,yy,zz] = getPoint(x + xx, y + yy, z + zz, cellSize, points, surface);
				}
			}	
		}

		addEdge(this.points[0,0,0], this.points[1,0,0], surface, edges);
		addEdge(this.points[1,0,0], this.points[1,1,0], surface, edges);
		addEdge(this.points[1,1,0], this.points[0,1,0], surface, edges);
		addEdge(this.points[0,1,0], this.points[0,0,0], surface, edges);

		addEdge(this.points[0,0,1], this.points[1,0,1], surface, edges);
		addEdge(this.points[1,0,1], this.points[1,1,1], surface, edges);
		addEdge(this.points[1,1,1], this.points[0,1,1], surface, edges);
		addEdge(this.points[0,1,1], this.points[0,0,1], surface, edges);
		
		addEdge(this.points[0,0,0], this.points[0,0,1], surface, edges);
		addEdge(this.points[0,1,0], this.points[0,1,1], surface, edges);
		addEdge(this.points[1,0,0], this.points[1,0,1], surface, edges);
		addEdge(this.points[1,1,0], this.points[1,1,1], surface, edges);
	}

	Point getPoint(int x, int y, int z, float cellSize, Point[,,] points, IIsoSurface surface){
		Point point = points[x,y,z];
		if(point == null){
			Vector3 p = new Vector3(x * cellSize,y * cellSize,z * cellSize);
			point = new Point(surface.sample(p.x,p.y,p.z), p);
			points[x,y,z] = point;
		}

		return point;
	}

	void addEdge(Point A, Point B, IIsoSurface surface, List<Edge> edges)
	{
		if((A.iso > 0 && B.iso > 0) || (A.iso < 0 && B.iso < 0)) 
			return;

		edges.Add(A.getEdge(B, surface, edges));
	}
}
