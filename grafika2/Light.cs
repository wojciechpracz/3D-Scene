using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    class Light
    {
        public Vec3D lightDirection;

        public Light(Vec3D lightDirection)
        {
            this.lightDirection = lightDirection;
            this.lightDirection = Vec3D.VectorNormalise(lightDirection);
        }

        public float GetLightIntensityForSurface(Vec3D surfaceNormal)
        {
            float intensity = (float)Math.Max(0.1f, Vec3D.VectorDotProduct(lightDirection, surfaceNormal));
            return intensity;
        }

    }
}
