using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


delegate void CellHandler(Cell cell);
delegate void EdgeHandler(Cell [] edge, int dimensionMap);

class Cell {
    double originalSize;
    double size;
    public int level;
    int maxLevel;
    Vector<double> pos;
    Cell parent;
    Cell [] children;
    double [] points;

    bool signed = false;


    QEF qef;
    public Vector<double> Point {
        get {
            if(qef == null && P != null) SolveQEF();
            if(qef == null) return null;
            return qef.solution;
        }
    }
    List<Vector<double>> P;
    List<Vector<double>> N;

    public void addEdge(Vector<double> p, Vector<double> n) {
        if(P == null) {
            P = new List<Vector<double>>();
            N = new List<Vector<double>>();
        }

        P.Add(p);
        N.Add(n);
    }


    public int Dimensions { 
        get{
            return pos.Count;
        }
    }

    public void SolveQEF() {
        if(P == null) return;

        if(P.Count < 4) {
            qef = null;
            return;
        }

        qef = new QEF(P,N);
    }

    public static Cell Create(Vector<double> pos, double size, IsoSurface surface, int maxLevel) {
        return new Cell(size, 1, maxLevel, pos, surface, null);
    }

    Cell(double originalSize, int level, int maxLevel, Vector<double> pos, IsoSurface surface, Cell parent) {
        this.originalSize = originalSize;
        this.level = level;
        this.maxLevel = maxLevel;
        this.pos = pos;
        this.parent = parent;

        size = originalSize / Math.Pow(2, level);

        points = new double[(int)Math.Pow(2, pos.Count)];

        int sign = 0;

        for(int i = 0; i < points.Length; i++) {
            var vector = posToVector(pointIndexToPos(i));
            points[i] = surface.sample(pos + vector);

            if(signed) continue;

            int thisSign = Math.Sign(points[i]);
            if(sign == 0) {
                sign = thisSign;
            }

            if(sign != thisSign) {
                setSigned();
            }

            sign = thisSign;
        }

        if(!signed) {
            if(parent != null) {
                if(parent.signed) return;
            }
        }

        if(level == maxLevel) {
        } else {
            children = new Cell[points.Length];
            for(int i = 0; i < children.Length; i++) {
                var vector = posToVector(pointIndexToPos(i)) * 0.5;
                children[i] = new Cell(originalSize, level + 1, maxLevel, pos + vector, surface, this);
            }
        }
    }

    public void EnumerateEdges(EdgeHandler handler) {
        proc(new Cell[]{this}, 0, handler, 1);
    }

    void proc(Cell[] cells, int dimensionMap, EdgeHandler handler, int level) {
        foreach(Cell cell in cells){
            if(!cell.signed) return;
        }

        if(cells.Length == children.Length / 2) {//only edges
            if(level == maxLevel) handler(cells, dimensionMap);
        }

        foreach(Cell cell in cells) {
            if(cell.children == null) return;
        }

        //get "block"
        Cell[] block = new Cell[children.Length];
        for(int i = 0; i < block.Length; i++) {
            var cellToPickFrom = i & dimensionMap;
            var cellToPick = i ^ dimensionMap;
            
            int actualCellToPickFrom = BitHelper.ReduceDims(cellToPickFrom, dimensionMap, Dimensions);

            block[i] = cells[actualCellToPickFrom].children[cellToPick];
        }

        for(int dimMap = 0; dimMap < children.Length - 1; dimMap++) {
            if((dimMap & dimensionMap) != dimensionMap) continue;

            int activeDimensions = 0;
            for(int i = 0; i < Dimensions; i++) {
                if(BitHelper.Get(dimMap, i)) {
                    activeDimensions++;
                }
            }

            for(int i = 0; i < children.Length; i++) {
                if((dimMap & i) > 0) continue;
                Cell[] pickedCells = new Cell[(int)Math.Pow(2,activeDimensions)];
                for(int j = 0; j < children.Length; j++) {
                    if((~dimMap & j) > 0) continue;
                    pickedCells[BitHelper.ReduceDims(j,dimMap,Dimensions)] = block[i | j];
                }
                proc(pickedCells, dimMap, handler, level + 1);
            }
        }
    }

    void setSigned() {
        signed = true;
        if(parent != null) {
            if(!parent.signed) {
                parent.setSigned();
            }
        }
    }

    public Vector<double> pointIndexToWorldPos(int index) {
        return pos + posToVector(pointIndexToPos(index));
    }

    public void ForEach(CellHandler foo) {

        if(children != null){
            foreach(Cell child in children) {
                child.ForEach(foo);
            }
        }
        
        if(true) {
            foo(this);
        }
    }

    public Vector<double> posToVector(bool [] pos) {
        return Vector<double>.Build.Dense(pos.Length, i => pos[i] ? 1 : 0) * size;
    }

    public bool [] pointIndexToPos(int posIndex) {
        bool [] pos = new bool[this.pos.Count];

        for(int i = 0; i < pos.Length; i++) {
            pos[i] = (posIndex & (1 << i)) > 0;
        }

        return pos;
    }

    public int getPointIndex(bool [] pos) {
        int i = 0;
        int pow = 1;

        foreach(bool b in pos) {
            if(b) {
                i += (int)Math.Pow(2,pow);
            }
        }

        return i;
    }

    public double getPoint(bool [] pos) {
        return points[getPointIndex(pos)];
    }

    public double getPoint(int i) {
        return points[i];
    }

    void setPoint(bool [] pos, double val) {
        points[getPointIndex(pos)] = val;
    }
}