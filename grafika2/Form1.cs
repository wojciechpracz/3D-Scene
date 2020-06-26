using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafika2
{
    public partial class Form1 : Form
    {
        private Mesh scene;
        private Transformations transformations;

        private float elapsedTime;

        private Vec3D vCamera = new Vec3D();
        private Vec3D vLookDir = new Vec3D();
        Vec3D vForward;
        float fYaw;

        Stopwatch stopwatch;

        private Bitmap bm;
        public Form1()
        {
            InitializeComponent();
            scene = new Mesh("scene2.obj");
            bm = new Bitmap(800, 800);
            transformations = new Transformations(bm.Width, bm.Height);
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            PainterHelper.ClearScreen(bm);

            elapsedTime = (float)stopwatch.Elapsed.TotalMilliseconds * 0.000021F;
            vForward = Vec3D.VectorMul(vLookDir, 0.05F);

            List<Triangle> trianglesToRaster = new List<Triangle>();



            Vec3D vUp = new Vec3D { x = 0, y = 1, z = 0 };
            Vec3D vTarget = new Vec3D { x = 0, y = 0, z = 1 };
            Matrix matCameraRot = Matrix.Matrix_MakeRotationY(fYaw);
            vLookDir = Matrix.MatrixMultiplyVector(vTarget, matCameraRot);
            vTarget = Vec3D.VectorAdd(vCamera, vLookDir);
            Matrix matCamera = Matrix.Matrix_PointAt(vCamera, vTarget, vUp);

            Matrix matView = Matrix.Matrix_QuickInverse(matCamera);


            foreach (var tri in scene.triangles)
            {
                Triangle triProjected, triTransformed, triViewed;

                triProjected = new Triangle(3);
                triTransformed = new Triangle(3);
                triViewed = new Triangle(3);


                triTransformed.points[0] = Matrix.MatrixMultiplyVector(tri.points[0], transformations.matWorld);
                triTransformed.points[1] = Matrix.MatrixMultiplyVector(tri.points[1], transformations.matWorld);
                triTransformed.points[2] = Matrix.MatrixMultiplyVector(tri.points[2], transformations.matWorld);

                Vec3D normal, line1, line2;

                normal = new Vec3D();
                line1 = new Vec3D();
                line2 = new Vec3D();

                line1 = Vec3D.VectorSub(triTransformed.points[1], triTransformed.points[0]);
                line2 = Vec3D.VectorSub(triTransformed.points[2], triTransformed.points[0]);

                normal = Vec3D.VectorCrossProduct(line1, line2);

                normal = Vec3D.VectorNormalise(normal);

                Vec3D vCameraRay = Vec3D.VectorSub(triTransformed.points[0], vCamera);

                //if(normal.z < 0)
                if (Vec3D.VectorDotProduct(normal, vCameraRay) < 0.0F)
                {
                    Vec3D light_direction = new Vec3D{ x = 0.0f, y = 1.0f, z = -1.0f };
                    light_direction = Vec3D.VectorNormalise(light_direction);

                    float dp = (float)Math.Max(0.1f, Vec3D.VectorDotProduct(light_direction, normal));

                    Color color = PainterHelper.ChangeColorBrightness(Color.Black, dp);

                    triViewed.points[0] = Matrix.MatrixMultiplyVector(triTransformed.points[0], matView);
                    triViewed.points[1] = Matrix.MatrixMultiplyVector(triTransformed.points[1], matView);
                    triViewed.points[2] = Matrix.MatrixMultiplyVector(triTransformed.points[2], matView);

                    // Project triangles from 3D --> 2D
                    triProjected.points[0] = Matrix.MatrixMultiplyVector(triViewed.points[0], transformations.matProj);
                    triProjected.points[1] = Matrix.MatrixMultiplyVector(triViewed.points[1], transformations.matProj);
                    triProjected.points[2] = Matrix.MatrixMultiplyVector(triViewed.points[2], transformations.matProj);

                    triProjected.points[0] = Vec3D.VectorDiv(triProjected.points[0], triProjected.points[0].w);
                    triProjected.points[1] = Vec3D.VectorDiv(triProjected.points[1], triProjected.points[1].w);
                    triProjected.points[2] = Vec3D.VectorDiv(triProjected.points[2], triProjected.points[2].w);

                    // X/Y are inverted so put them back
                    triProjected.points[0].x *= -1.0f;
                    triProjected.points[1].x *= -1.0f;
                    triProjected.points[2].x *= -1.0f;
                    triProjected.points[0].y *= -1.0f;
                    triProjected.points[1].y *= -1.0f;
                    triProjected.points[2].y *= -1.0f;

                    // Scale into view
                    Vec3D vOffsetView = new Vec3D { x = 1, y = 1, z = 0 };

                    triProjected.points[0] = Vec3D.VectorAdd(triProjected.points[0], vOffsetView);
                    triProjected.points[1] = Vec3D.VectorAdd(triProjected.points[1], vOffsetView);
                    triProjected.points[2] = Vec3D.VectorAdd(triProjected.points[2], vOffsetView);

                    triProjected.points[0].x *= 0.5F * (float)bm.Width;
                    triProjected.points[0].y *= 0.5F * (float)bm.Height;
                    triProjected.points[1].x *= 0.5F * (float)bm.Width;
                    triProjected.points[1].y *= 0.5F * (float)bm.Height;
                    triProjected.points[2].x *= 0.5F * (float)bm.Width;
                    triProjected.points[2].y *= 0.5F * (float)bm.Height;

                    //PainterHelper.DrawTriangle(triProjected, bm, color);

                    //pictureBoxCanvas.Image = bm;
                    triProjected.color = color;

                    triProjected.zTotal = (triProjected.points[0].z + triProjected.points[1].z + triProjected.points[2].z) / 3.0F;

                    trianglesToRaster.Add(triProjected);
                }

            }

            trianglesToRaster.Sort((p,q) => q.zTotal.CompareTo(p.zTotal));

            foreach (var triangle in trianglesToRaster)
            {
                PainterHelper.DrawTriangle(triangle, bm);

                pictureBoxCanvas.Image = bm;
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                vCamera.y += 0.05F;/* 2.0F * elapsedTime;*/


            if (e.KeyCode == Keys.Down)
                vCamera.y -= 0.05F;

            if (e.KeyCode == Keys.Left)
                vCamera.x += 0.05F;


            if (e.KeyCode == Keys.Right)
                vCamera.x -= 0.05F;


            if (e.KeyCode == Keys.W)
                vCamera = Vec3D.VectorAdd(vCamera, vForward);

            if (e.KeyCode == Keys.S)
                vCamera = Vec3D.VectorSub(vCamera, vForward);

            if (e.KeyCode == Keys.A)
                fYaw -= 0.05F;

            if (e.KeyCode == Keys.D)
                fYaw += 0.05F;

        }
    }
}
