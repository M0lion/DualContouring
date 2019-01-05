using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

delegate void CellHandler(Cell cell);

class Cell {
    double [] points;
    double originalSize;
    public int level;

    double size;

    Vector<double> pos;

    Cell parent;
    Cell [] children;

    bool signed = false;

    public int Dimensions { 
        get{
            return pos.Count;
        }
    }

    public static Cell Create(Vector<double> pos, double size, IsoSurface surface, int maxLevel) {
        return new Cell(size, 1, maxLevel, pos, surface, null);
    }

    Cell(double originalSize, int level, int maxLevel, Vector<double> pos, IsoSurface surface, Cell parent) {
        this.originalSize = originalSize;
        this.level = level;
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
            // MAX LEVEL
        } else {
            children = new Cell[points.Length];
            for(int i = 0; i < children.Length; i++) {
                var vector = posToVector(pointIndexToPos(i)) * 0.5;
                children[i] = new Cell(originalSize, level + 1, maxLevel, pos + vector, surface, this);
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

    void setPoint(bool [] pos, double val) {
        points[getPointIndex(pos)] = val;
    }
}