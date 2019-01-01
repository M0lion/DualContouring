using UnityEngine;

class Cell {
    double [] points;
    double originalSize;
    int level;

    Cell [] children;

    public Cell(double [] points, double originalSize, int level, int maxLevel, Vector3d pos, Surface surface) {
        this.points = points;
        this.originalSize = originalSize;
        this.level = level;

        double size = originalSize / level;

        if(level == maxLevel) {
            // MAX LEVEL
        } else {
        }
    }
}