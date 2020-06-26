using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafika2
{
    class Camera
    {
        public Vec3D vCamera;
        public Vec3D vLookDir;
        public Vec3D vForward;
        public float fYaw;

        public Vec3D vUp;
        public Vec3D vTarget; 
        public Camera()
        {
            vCamera = new Vec3D();
            vLookDir = new Vec3D();
            vForward = new Vec3D();

            vUp = new Vec3D { x = 0, y = 1, z = 0 };
            vTarget = new Vec3D { x = 0, y = 0, z = 1 };
        }

        public Matrix RotateCamera()
        {
            vTarget = new Vec3D { x = 0, y = 0, z = 1 };
            Matrix matCameraRot = Matrix.Matrix_MakeRotationY(fYaw);
            vLookDir = Matrix.MatrixMultiplyVector(vTarget, matCameraRot);
            vTarget = Vec3D.VectorAdd(vCamera, vLookDir);
            Matrix matCamera = Matrix.Matrix_PointAt(vCamera,vTarget, vUp);
            Matrix matView = Matrix.Matrix_QuickInverse(matCamera);

            return matView;
        }

        public void MoveCamera(Keys key)
        {
            if (key == Keys.Up)
                vCamera.y += 0.05F;

            if (key == Keys.Down)
                vCamera.y -= 0.05F;

            if (key == Keys.Left)
                vCamera.x += 0.05F;

            if (key == Keys.Right)
                vCamera.x -= 0.05F;

            if (key == Keys.W)
                vCamera = Vec3D.VectorAdd(vCamera, vForward);

            if (key == Keys.S)
                vCamera = Vec3D.VectorSub(vCamera, vForward);

            if (key == Keys.A)
                fYaw -= 0.05F;

            if (key == Keys.D)
                fYaw += 0.05F;
        }
    }
}
