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
            points = new Vec3D[] { new Vec3D (), new Vec3D(), new Vec3D() } ;
            color = Color.Black;
            zTotal = 0;
        }

        public Vec3D Normal 
        { 
            get 
            {
                Vec3D normal, line1, line2;

                normal = new Vec3D();
                line1 = new Vec3D();
                line2 = new Vec3D();

                line1 = Vec3D.VectorSub(points[1], points[0]);
                line2 = Vec3D.VectorSub(points[2], points[0]);

                normal = Vec3D.VectorCrossProduct(line1, line2);

                normal = Vec3D.VectorNormalise(normal);

                return normal;
            } 
        }

        public void InvertXAndY() 
        {
            points[0].x *= -1.0f;
            points[1].x *= -1.0f;
            points[2].x *= -1.0f;
            points[0].y *= -1.0f;
            points[1].y *= -1.0f;
            points[2].y *= -1.0f;
        }

        public static Triangle ProjectTriangle(Matrix matProj, Triangle triToProject)
        {
            Triangle triProjected = new Triangle(3);

            triProjected.points[0] = Matrix.MatrixMultiplyVector(triToProject.points[0], matProj);
            triProjected.points[1] = Matrix.MatrixMultiplyVector(triToProject.points[1], matProj);
            triProjected.points[2] = Matrix.MatrixMultiplyVector(triToProject.points[2], matProj);

            triProjected.points[0] = Vec3D.VectorDiv(triProjected.points[0], triProjected.points[0].w);
            triProjected.points[1] = Vec3D.VectorDiv(triProjected.points[1], triProjected.points[1].w);
            triProjected.points[2] = Vec3D.VectorDiv(triProjected.points[2], triProjected.points[2].w);

            return triProjected;
        }

        public static Triangle TransformTriangle(Triangle tri, Matrix matWorld)
        {
            Triangle triTransformed = new Triangle(3);

            triTransformed.points[0] = Matrix.MatrixMultiplyVector(tri.points[0], matWorld);
            triTransformed.points[1] = Matrix.MatrixMultiplyVector(tri.points[1], matWorld);
            triTransformed.points[2] = Matrix.MatrixMultiplyVector(tri.points[2], matWorld);

            return triTransformed;
        }

        public void Offset(Vec3D vOffsetView, Bitmap bm)
        {
            points[0] = Vec3D.VectorAdd(points[0], vOffsetView);
            points[1] = Vec3D.VectorAdd(points[1], vOffsetView);
            points[2] = Vec3D.VectorAdd(points[2], vOffsetView);

            points[0].x *= 0.5F * (float)bm.Width;
            points[0].y *= 0.5F * (float)bm.Height;
            points[1].x *= 0.5F * (float)bm.Width;
            points[1].y *= 0.5F * (float)bm.Height;
            points[2].x *= 0.5F * (float)bm.Width;
            points[2].y *= 0.5F * (float)bm.Height;
        }
    }
}
