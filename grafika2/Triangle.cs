using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    struct Triangle
    {
        public Vec3D[] points;
        public Color color;

        public float zTotal;
        public Triangle(int arraySize = 3)
        {
            points = new Vec3D[arraySize];
            color = Color.Black;
            zTotal = 0;
        }
    }
}
