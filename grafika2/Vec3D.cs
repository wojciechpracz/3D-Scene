using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    public class Vec3D
    {
        public float x, y, z;
        public float w = 1;

        public static Vec3D VectorMul(Vec3D v1, float k)
        {
            return new Vec3D { x = v1.x * k, y = v1.y * k, z = v1.z * k };
        }


        public static Vec3D VectorAdd(Vec3D v1, Vec3D v2)
        {
            return new Vec3D { x = v1.x + v2.x, y = v1.y + v2.y, z = v1.z + v2.z };
        }

        public static Vec3D VectorSub(Vec3D v1, Vec3D v2)
        {
            return new Vec3D { x = v1.x - v2.x, y = v1.y - v2.y, z = v1.z - v2.z };
        }


        public static Vec3D VectorDiv(Vec3D v1, float k)
        {
            return new Vec3D { x = v1.x / k, y = v1.y / k, z = v1.z / k };
        }

        public static float VectorDotProduct(Vec3D v1, Vec3D v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static float VectorLength(Vec3D v)
        {
            return (float)Math.Sqrt(VectorDotProduct(v, v));
        }

        public static Vec3D VectorNormalise(Vec3D v)
        {
            float l = VectorLength(v);
            return new Vec3D { x = v.x / l, y = v.y / l, z = v.z / l };
        }

        public static Vec3D VectorCrossProduct(Vec3D v1, Vec3D v2)
        {
            Vec3D v = new Vec3D();

            v.x = v1.y * v2.z - v1.z * v2.y;
            v.y = v1.z * v2.x - v1.x * v2.z;
            v.z = v1.x * v2.y - v1.y * v2.x;

            return v;
        }

    }
}
