using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    struct Mesh
    {
        public List<Triangle> triangles;

        public bool LoadFromObject(string sFileName)
        {
            List<Vec3D> verts = new List<Vec3D>();

            if(triangles == null)
            {
                triangles = new List<Triangle>();
            }

            using (StreamReader sr = new StreamReader(sFileName))
            {

                string line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    if (line[0] == 'v')
                    {
                        Vec3D vect;
                        string[] parts = line.Split(' ');


                        vect.x = float.Parse(parts[1], CultureInfo.InvariantCulture.NumberFormat);
                        vect.y = float.Parse(parts[2], CultureInfo.InvariantCulture.NumberFormat);
                        vect.z = float.Parse(parts[3], CultureInfo.InvariantCulture.NumberFormat);

                        verts.Add(vect);
                    }

                    if (line[0] == 'f')
                    {
                        int[] f = new int[3];
                        string[] parts = line.Split(' ');

                        f[0] = Convert.ToInt32(parts[1]);
                        f[1] = Convert.ToInt32(parts[2]);
                        f[2] = Convert.ToInt32(parts[3]);

                        triangles.Add(new Triangle { points = new Vec3D[] { verts[f[0] - 1], verts[f[1] - 1], verts[f[2] - 1] } });
                    }

                    line = sr.ReadLine();
                }


            }

            return true;
        }
    }

}
