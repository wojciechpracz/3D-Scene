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

        Camera camera;
        Light light;

        private Bitmap bm;
        public Form1()
        {
            InitializeComponent();
            scene = new Mesh("scene.obj");
            bm = new Bitmap(800, 800);
            camera = new Camera();
            light = new Light(new Vec3D{ x = 0.0f, y = 1.0f, z = -1.0f });
            transformations = new Transformations(bm.Width, bm.Height);

        }

        private void Render(object sender, EventArgs e)
        {
            PainterHelper.ClearScreen(bm);

            camera.vForward = Vec3D.VectorMul(camera.vLookDir, 0.05F);

            List<Triangle> trianglesToRaster = new List<Triangle>();

            Matrix matView = camera.RotateCamera();

            foreach (var tri in scene.triangles)
            {
                Triangle triProjected, triTransformed, triViewed;

                triViewed = new Triangle(3);

                triTransformed = Triangle.TransformTriangle(tri, transformations.matWorld);

                Vec3D vCameraRay = Vec3D.VectorSub(triTransformed.points[0], camera.vCamera);

                if (Vec3D.VectorDotProduct(triTransformed.Normal, vCameraRay) < 0.0F)
                {
                    float lightIntensity = light.GetLightIntensityForSurface(triTransformed.Normal);

                    Color color = PainterHelper.ChangeColorBrightness(Color.Black, lightIntensity);

                    triViewed = Triangle.TransformTriangle(triTransformed, matView);

                    triProjected = Triangle.ProjectTriangle(transformations.matProj, triViewed);

                    triProjected.InvertXAndY();

                    //// Scale into view
                    Vec3D vOffsetView = new Vec3D { x = 1, y = 1, z = 0 };

                    triProjected.Offset(vOffsetView, bm);

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
            camera.MoveCamera(e.KeyCode);
        }
    }
}
