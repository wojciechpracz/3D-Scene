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
        private Mesh meshCube;
        private Matrix matRotZ = new Matrix (4);
        private Matrix matRotX = new Matrix(4);
        private Matrix matProj = new Matrix(4);

        Stopwatch stopwatch = new Stopwatch();


        float fTheta;


        private Bitmap bm = new Bitmap(400, 400);

        public Form1()
        {
            InitializeComponent();

            stopwatch.Start();

            pictureBoxCanvas.Image = bm;
            
        }

        private Mesh CreateCube()
        {
            Mesh meshCube = new Mesh
            {
                triangles = new List<Triangle>
                {
                    //SOUTH
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f }
                        }
                    },
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f }
                        }
                    },

                    //EAST
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f }
                        }
                    },
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f }
                        }
                    },

                    //NORTH
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f }
                        }
                    },
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f }
                        }
                    },
                   
                    //WEST
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f }
                        }
                    },
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f }
                        }
                    },                   
                    
                    //TOP
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f, },
                            new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f }
                        }
                    },
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f },
                            new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f }
                        }
                    },

                                        
                    //BOTTOM
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f, },
                            new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f }
                        }
                    },
                    new Triangle
                    {
                        points = new Vec3D[]
                        {
                            new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f },
                            new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f },
                            new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f }
                        }
                    },
                }
            };

            return meshCube;
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            using (Graphics gfx = Graphics.FromImage(bm))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                gfx.FillRectangle(brush, 0, 0, bm.Width, bm.Height);
            };

            fTheta = 0.0015F * (float)stopwatch.Elapsed.TotalMilliseconds;
            
            float fNear = 0.1F;
            float fFar = 1000.0F;
            float fFov = 90.0F;

            float fAspectRatio = (float)bm.Height / (float)bm.Width;

            float fFovRad = (float)(1.0 / Math.Tan(fFov * 0.5 / 180.0 * 3.14159));

            matProj.matrix[0, 0] = fAspectRatio * fFovRad;
            matProj.matrix[1, 1] = fFovRad;
            matProj.matrix[2, 2] = fFar / (fFar - fNear);
            matProj.matrix[3, 2] = (-fFar * fNear) / (fFar - fNear);
            matProj.matrix[2, 3] = 1.0F;
            matProj.matrix[3, 3] = 0.0F;

            meshCube = CreateCube();

            // Rotation Z
            matRotZ.matrix[0, 0] = (float)Math.Cos(fTheta);
            matRotZ.matrix[0, 1] = (float)Math.Sin(fTheta);
            matRotZ.matrix[1, 0] = (float)(Math.Sin(fTheta) * -1);
            matRotZ.matrix[1, 1] = (float)Math.Cos(fTheta);
            matRotZ.matrix[2, 2] = 1;
            matRotZ.matrix[3, 3] = 1;

            // Rotation X
            matRotX.matrix[0, 0] = 1;
            matRotX.matrix[1, 1] = (float)Math.Cos(fTheta * 0.5F);
            matRotX.matrix[1, 2] = (float)Math.Sin(fTheta * 0.5F);
            matRotX.matrix[2, 1] = (float)(Math.Sin(fTheta * 0.5F) * -1);
            matRotX.matrix[2, 2] = (float)Math.Cos(fTheta * 0.5F);
            matRotX.matrix[3, 3] = 1;

            foreach (var tri in meshCube.triangles)
            {
                Triangle triProjected, triTranslated, triRotatedZ, triRotatedZX;

                triProjected = new Triangle(3);
                triTranslated = new Triangle(3);
                triRotatedZ = new Triangle(3);
                triRotatedZX = new Triangle(3);

                // Rotate in Z-Axiss
                Matrix.MultiplyMatrixVector(tri.points[0], out triRotatedZ.points[0], matRotZ);
                Matrix.MultiplyMatrixVector(tri.points[1], out triRotatedZ.points[1], matRotZ);
                Matrix.MultiplyMatrixVector(tri.points[2], out triRotatedZ.points[2], matRotZ);

                // Rotate in X-Axis
                Matrix.MultiplyMatrixVector(triRotatedZ.points[0], out triRotatedZX.points[0], matRotX);
                Matrix.MultiplyMatrixVector(triRotatedZ.points[1], out triRotatedZX.points[1], matRotX);
                Matrix.MultiplyMatrixVector(triRotatedZ.points[2], out triRotatedZX.points[2], matRotX);

                // Offset into the screen
                triTranslated = triRotatedZX;
                triTranslated.points[0].z = triRotatedZX.points[0].z + 3.0F;
                triTranslated.points[1].z = triRotatedZX.points[1].z + 3.0F;
                triTranslated.points[2].z = triRotatedZX.points[2].z + 3.0F;

                // Project triangles from 3D --> 2D
                Matrix.MultiplyMatrixVector(triTranslated.points[0], out triProjected.points[0], matProj);
                Matrix.MultiplyMatrixVector(triTranslated.points[1], out triProjected.points[1], matProj);
                Matrix.MultiplyMatrixVector(triTranslated.points[2], out triProjected.points[2], matProj);

                // Scale into view
                triProjected.points[0].x += 1.0F; triProjected.points[0].y += 1.0F;
                triProjected.points[1].x += 1.0F; triProjected.points[1].y += 1.0F;
                triProjected.points[2].x += 1.0F; triProjected.points[2].y += 1.0F;
                triProjected.points[0].x *= 0.5F * (float)bm.Width;
                triProjected.points[0].y *= 0.5F * (float)bm.Height;
                triProjected.points[1].x *= 0.5F * (float)bm.Width;
                triProjected.points[1].y *= 0.5F * (float)bm.Height;
                triProjected.points[2].x *= 0.5F * (float)bm.Width;
                triProjected.points[2].y *= 0.5F * (float)bm.Height;

                PainterHelper.DrawTriangle(triProjected, bm);

                pictureBoxCanvas.Image = bm;

            }
        }
    }
}
