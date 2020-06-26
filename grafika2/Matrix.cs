using System;
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

        public static Vec3D MatrixMultiplyVector(Vec3D i, Matrix m)
        {
            Vec3D v;

            v = new Vec3D();

            v.x = i.x * m.matrix[0, 0] + i.y * m.matrix[1, 0] + i.z * m.matrix[2, 0] + i.w * m.matrix[3, 0];
            v.y = i.x * m.matrix[0, 1] + i.y * m.matrix[1, 1] + i.z * m.matrix[2, 1] + i.w * m.matrix[3, 1];
            v.z = i.x * m.matrix[0, 2] + i.y * m.matrix[1, 2] + i.z * m.matrix[2, 2] + i.w * m.matrix[3, 2];
            v.w = i.x * m.matrix[0, 3] + i.y * m.matrix[1, 3] + i.z * m.matrix[2, 3] + i.w * m.matrix[3, 3];
            return v;
        }

        public static void MultiplyMatrixVector(Vec3D i, Vec3D output, Matrix m)
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

        public static Matrix Matrix_MakeIdentity()
        {
            Matrix matrix = new Matrix(4);
            matrix.matrix[0, 0] = 1.0f;
            matrix.matrix[1, 1] = 1.0f;
            matrix.matrix[2, 2] = 1.0f;
            matrix.matrix[3, 3] = 1.0f;
            return matrix;
        }

        public static Matrix Matrix_MakeRotationX(float fAngleRad)
        {
            Matrix matRotX = new Matrix(4);
            matRotX.matrix[0, 0] = 1;
            matRotX.matrix[1, 1] = (float)Math.Cos(fAngleRad);
            matRotX.matrix[1, 2] = (float)Math.Sin(fAngleRad);
            matRotX.matrix[2, 1] = (float)(Math.Sin(fAngleRad) * -1);
            matRotX.matrix[2, 2] = (float)Math.Cos(fAngleRad);
            matRotX.matrix[3, 3] = 1;
            return matRotX;
        }

        public static Matrix Matrix_MakeRotationY(float fAngleRad)
        {
            Matrix matRotY = new Matrix(4);
            matRotY.matrix[0, 0] = (float)Math.Cos(fAngleRad);
            matRotY.matrix[0, 2] = (float)Math.Sin(fAngleRad);
            matRotY.matrix[2, 0] = (float)(Math.Sin(fAngleRad) * -1);
            matRotY.matrix[1, 1] = 1.0F;
            matRotY.matrix[2, 2] = (float)Math.Cos(fAngleRad * 0.5F);
            matRotY.matrix[3, 3] = 1.0F;
            return matRotY;
        }

        public static Matrix Matrix_MakeRotationZ(float fAngleRad)
        {
            Matrix matRotZ = new Matrix(4);

            matRotZ.matrix[0, 0] = (float)Math.Cos(fAngleRad);
            matRotZ.matrix[0, 1] = (float)Math.Sin(fAngleRad);
            matRotZ.matrix[1, 0] = (float)(Math.Sin(fAngleRad) * -1);
            matRotZ.matrix[1, 1] = (float)Math.Cos(fAngleRad);
            matRotZ.matrix[2, 2] = 1;
            matRotZ.matrix[3, 3] = 1;

            return matRotZ;
        }

        public static Matrix Matrix_MakeTranslation(float x, float y, float z)
        {
            Matrix matrix = new Matrix(4);

            matrix.matrix[0, 0] = 1.0f;
            matrix.matrix[1, 1] = 1.0f;
            matrix.matrix[2, 2] = 1.0f;
            matrix.matrix[3, 3] = 1.0f;
            matrix.matrix[3, 0] = x;
            matrix.matrix[3, 1] = y;
            matrix.matrix[3, 2] = z;

            return matrix;
        }

        public static Matrix Matrix_MakeProjection(float fFovDegrees, float fAspectRatio, float fNear, float fFar)
        {
            float fFovRad = 1.0f / (float)Math.Tan(fFovDegrees * 0.5f / 180.0f * 3.14159f);
            Matrix matrix = new Matrix(4);
            matrix.matrix[0, 0] = fAspectRatio * fFovRad;
            matrix.matrix[1, 1] = fFovRad;
            matrix.matrix[2, 2] = fFar / (fFar - fNear);
            matrix.matrix[3, 2] = (-fFar * fNear) / (fFar - fNear);
            matrix.matrix[2, 3] = 1.0f;
            matrix.matrix[3, 3] = 0.0f;
            return matrix;
        }


        public static Matrix Matrix_MultiplyMatrix(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix(4);
            for (int c = 0; c < 4; c++)
                for (int r = 0; r < 4; r++)
                    matrix.matrix[r, c] = m1.matrix[r, 0] * m2.matrix[0, c] + m1.matrix[r, 1] * m2.matrix[1, c] + m1.matrix[r, 2] * m2.matrix[2, c] + m1.matrix[r, 3] * m2.matrix[3, c];
            return matrix;
        }

        public static Matrix Matrix_PointAt(Vec3D pos, Vec3D target, Vec3D up)
        {
            Vec3D newForward = Vec3D.VectorSub(target, pos);
            newForward = Vec3D.VectorNormalise(newForward);

            Vec3D a = Vec3D.VectorMul(newForward, Vec3D.VectorDotProduct(up, newForward));
            Vec3D newUp = Vec3D.VectorSub(up, a);
            newUp = Vec3D.VectorNormalise(newUp);

            Vec3D newRight = Vec3D.VectorCrossProduct(newUp, newForward);

            Matrix matrix = new Matrix(4);

            matrix.matrix[0, 0] = newRight.x; matrix.matrix[0, 1] = newRight.y; matrix.matrix[0, 2] = newRight.z; matrix.matrix[0, 3] = 0.0f;
            matrix.matrix[1, 0] = newUp.x; matrix.matrix[1, 1] = newUp.y; matrix.matrix[1, 2] = newUp.z; matrix.matrix[1, 3] = 0.0f;
            matrix.matrix[2, 0] = newForward.x; matrix.matrix[2, 1] = newForward.y; matrix.matrix[2, 2] = newForward.z; matrix.matrix[2, 3] = 0.0f;
            matrix.matrix[3, 0] = pos.x; matrix.matrix[3, 1] = pos.y; matrix.matrix[3, 2] = pos.z; matrix.matrix[3, 3] = 1.0f;

            return matrix;
        }

        public static Matrix Matrix_QuickInverse(Matrix m) // Only for Rotation/Translation Matrices
        {
            Matrix matrix = new Matrix(4);
            matrix.matrix[0, 0] = m.matrix[0, 0]; matrix.matrix[0, 1] = m.matrix[1, 0]; matrix.matrix[0, 2] = m.matrix[2, 0]; matrix.matrix[0, 3] = 0.0f;
            matrix.matrix[1, 0] = m.matrix[0, 1]; matrix.matrix[1, 1] = m.matrix[1, 1]; matrix.matrix[1, 2] = m.matrix[2, 1]; matrix.matrix[1, 3] = 0.0f;
            matrix.matrix[2, 0] = m.matrix[0, 2]; matrix.matrix[2, 1] = m.matrix[1, 2]; matrix.matrix[2, 2] = m.matrix[2, 2]; matrix.matrix[2, 3] = 0.0f;
            matrix.matrix[3, 0] = -(m.matrix[3, 0] * matrix.matrix[0, 0] + m.matrix[3, 1] * matrix.matrix[1, 0] + m.matrix[3, 2] * matrix.matrix[2, 0]);
            matrix.matrix[3, 1] = -(m.matrix[3, 0] * matrix.matrix[0, 1] + m.matrix[3, 1] * matrix.matrix[1, 1] + m.matrix[3, 2] * matrix.matrix[2, 1]);
            matrix.matrix[3, 2] = -(m.matrix[3, 0] * matrix.matrix[0, 2] + m.matrix[3, 1] * matrix.matrix[1, 2] + m.matrix[3, 2] * matrix.matrix[2, 2]);
            matrix.matrix[3, 3] = 1.0f;
            return matrix;
        }

    }
}
