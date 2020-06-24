using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    struct Triangle
    {
        public Vec3D[] points;

        public Triangle(int arraySize = 3)
        {
            points = new Vec3D[arraySize];
        }
    }
}
