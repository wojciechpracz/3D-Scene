﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    struct Matrix
    {
        public float[,] matrix;

        public Matrix(int size)
        {
            matrix = new float[size, size];
        }

        public static void MultiplyMatrixVector(Vec3D i, out Vec3D output, Matrix m)
        {
            output.x = i.x * m.matrix[0, 0] + i.y * m.matrix[1, 0] + i.z * m.matrix[2, 0] + m.matrix[3, 0];
            output.y = i.x * m.matrix[0, 1] + i.y * m.matrix[1, 1] + i.z * m.matrix[2, 1] + m.matrix[3, 1];
            output.z = i.x * m.matrix[0, 2] + i.y * m.matrix[1, 2] + i.z * m.matrix[2, 2] + m.matrix[3, 2];
            float w = i.x * m.matrix[0, 3] + i.y * m.matrix[1, 3] + i.z * m.matrix[2, 3] + m.matrix[3, 3];

            if (w != 0.0f)
            {
                output.x /= w; output.y /= w; output.z /= w;
            }
        }
    }
}
