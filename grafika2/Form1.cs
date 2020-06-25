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
        private Matrix matRotZ;
        private Matrix matRotX;
        private Matrix matProj;
        private Matrix matTrans;
        private Matrix matWorld;


        private Vec3D vCamera = new Vec3D();

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
            //Mesh meshCube = new Mesh
            //{
            //    triangles = new List<Triangle>
            //    {
            //        //SOUTH
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f }
            //            }
            //        },
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f }
            //            }
            //        },

            //        //EAST
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f }
            //            }
            //        },
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f },
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f }
            //            }
            //        },

            //        //NORTH
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f }
            //            }
            //        },
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f }
            //            }
            //        },

            //        //WEST
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f }
            //            }
            //        },
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f },
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f }
            //            }
            //        },                   

            //        //TOP
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f, },
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 1.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f }
            //            }
            //        },
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 0.0f, y = 1.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 1.0f },
            //                new Vec3D { x = 1.0f, y = 1.0f, z = 0.0f }
            //            }
            //        },


            //        //BOTTOM
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f, },
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f }
            //            }
            //        },
            //        new Triangle
            //        {
            //            points = new Vec3D[]
            //            {
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 1.0f },
            //                new Vec3D { x = 0.0f, y = 0.0f, z = 0.0f },
            //                new Vec3D { x = 1.0f, y = 0.0f, z = 0.0f }
            //            }
            //        },
            //    }
            //};

            Mesh sphere = new Mesh();
            sphere.LoadFromObject("scene.obj");

            return sphere;
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            using (Graphics gfx = Graphics.FromImage(bm))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                gfx.FillRectangle(brush, 0, 0, bm.Width, bm.Height);
            };

            fTheta = 0.0005F * (float)stopwatch.Elapsed.TotalMilliseconds;

            List<Triangle> trianglesToRaster = new List<Triangle>();

            meshCube = CreateCube();
            matProj = Matrix_MakeProjection(90.0F, (float)bm.Width / (float)bm.Height, 0.1F, 1000.0F);
            matRotZ = Matrix_MakeRotationZ(fTheta * 0.5F);
            matRotX = Matrix_MakeRotationX(fTheta * 0.5F);
            matTrans = Matrix_MakeTranslation(0.0F, 0.0F, 5.0F);
            matWorld = Matrix_MakeIdentity();
            matWorld = Matrix_MultiplyMatrix(matRotZ, matRotX);
            matWorld = Matrix_MultiplyMatrix(matWorld, matTrans);

            foreach (var tri in meshCube.triangles)
            {
                Triangle triProjected, triTransformed;

                triProjected = new Triangle(3);
                triTransformed = new Triangle(3);


                triTransformed.points[0] = Matrix.MatrixMultiplyVector(tri.points[0], matWorld);
                triTransformed.points[1] = Matrix.MatrixMultiplyVector(tri.points[1], matWorld);
                triTransformed.points[2] = Matrix.MatrixMultiplyVector(tri.points[2], matWorld);

                Vec3D normal, line1, line2;

                normal = new Vec3D();
                line1 = new Vec3D();
                line2 = new Vec3D();

                line1 = VectorSub(triTransformed.points[1], triTransformed.points[0]);
                line2 = VectorSub(triTransformed.points[2], triTransformed.points[0]);

                normal = VectorCrossProduct(line1, line2);

                normal = VectorNormalise(normal);

                Vec3D vCameraRay = VectorSub(triTransformed.points[0], vCamera);

                //if(normal.z < 0)
                if (VectorDotProduct(normal, vCameraRay) < 0.0F)
                {
                    Vec3D light_direction = new Vec3D{ x = 0.0f, y = 1.0f, z = -1.0f };
                    light_direction = VectorNormalise(light_direction);

                    float dp = (float)Math.Max(0.1f, VectorDotProduct(light_direction, normal));

                    Color color = PainterHelper.ChangeColorBrightness(Color.Black, dp);

                    // Project triangles from 3D --> 2D
                    Matrix.MultiplyMatrixVector(triTransformed.points[0], triProjected.points[0], matProj);
                    Matrix.MultiplyMatrixVector(triTransformed.points[1], triProjected.points[1], matProj);
                    Matrix.MultiplyMatrixVector(triTransformed.points[2], triProjected.points[2], matProj);

                    // Scale into view
                    triProjected.points[0].x += 1.0F;
                    triProjected.points[0].y += 1.0F;

                    triProjected.points[1].x += 1.0F;
                    triProjected.points[1].y += 1.0F;

                    triProjected.points[2].x += 1.0F;
                    triProjected.points[2].y += 1.0F;

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

            //brightness += 0.01F;

            //using (Graphics gfx = Graphics.FromImage(bm))
            //using (SolidBrush brush = new SolidBrush(PainterHelper.ChangeColorBrightness(Color.Black, brightness)))
            //{
            //    gfx.FillRectangle(brush, 0, 0, bm.Width, bm.Height);
            //};

            //pictureBoxCanvas.Image = bm;

            Vec3D VectorAdd(Vec3D v1, Vec3D v2)
            {
                return new Vec3D { x = v1.x + v2.x, y = v1.y + v2.y, z = v1.z + v2.z };
            }

            Vec3D VectorSub(Vec3D v1, Vec3D v2)
            {
                return new Vec3D { x = v1.x - v2.x, y = v1.y - v2.y, z = v1.z - v2.z };
            }

            Vec3D VectorMul(Vec3D v1, float k)
            {
                return new Vec3D { x = v1.x * k, y = v1.y * k, z = v1.z * k };
            }

            Vec3D VectorDiv(Vec3D v1, float k)
            {
                return new Vec3D { x = v1.x / k, y = v1.y / k, z = v1.z / k };
            }

            float VectorDotProduct(Vec3D v1, Vec3D v2)
            {
                return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
            }

            float VectorLength(Vec3D v)
            {
                return (float)Math.Sqrt(VectorDotProduct(v, v));
            }

            Vec3D VectorNormalise(Vec3D v)
            {
                float l = VectorLength(v);
                return new Vec3D { x = v.x / l, y = v.y / l, z = v.z / l };
            }

            Vec3D VectorCrossProduct(Vec3D v1, Vec3D v2)
            {
                Vec3D v = new Vec3D();

                v.x = v1.y * v2.z - v1.z * v2.y;
                v.y = v1.z * v2.x - v1.x * v2.z;
                v.z = v1.x * v2.y - v1.y * v2.x;

                return v;
            }

            Matrix Matrix_MakeIdentity()
            {
                Matrix matrix = new Matrix(4);
                matrix.matrix[0, 0] = 1.0f;
                matrix.matrix[1, 1] = 1.0f;
                matrix.matrix[2, 2] = 1.0f;
                matrix.matrix[3, 3] = 1.0f;
                return matrix;
            }

            Matrix Matrix_MakeRotationX(float fAngleRad)
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

            Matrix Matrix_MakeRotationY(float fAngleRad)
            {
                Matrix matRotY = new Matrix(4);
                matRotY.matrix[0, 0] = (float)Math.Cos(fAngleRad);
                matRotY.matrix[0, 2] = (float)Math.Sin(fAngleRad);
                matRotY.matrix[2, 0] = (float)(Math.Sin(fAngleRad) * -1);
                matRotY.matrix[1, 1] = 1.0F;
                matRotY.matrix[2, 2] = (float)Math.Cos(fTheta * 0.5F);
                matRotY.matrix[3, 3] = 1.0F;
                return matRotY;
            }

            Matrix Matrix_MakeRotationZ(float fAngleRad)
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

            Matrix Matrix_MakeTranslation(float x, float y, float z)
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

            Matrix Matrix_MakeProjection(float fFovDegrees, float fAspectRatio, float fNear, float fFar)
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


            Matrix Matrix_MultiplyMatrix(Matrix m1, Matrix m2)
            {
                Matrix matrix = new Matrix(4);
                for (int c = 0; c < 4; c++)
                    for (int r = 0; r < 4; r++)
                        matrix.matrix[r, c] = m1.matrix[r, 0] * m2.matrix[0, c] + m1.matrix[r, 1] * m2.matrix[1, c] + m1.matrix[r, 2] * m2.matrix[2, c] + m1.matrix[r, 3] * m2.matrix[3, c];
                return matrix;
            }

        }
    }
}
