using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    class Transformations
    {
        public Matrix matProj;
        public Matrix matTrans;
        public Matrix matWorld;

        public Transformations(float width, float height)
        {
            matProj = Matrix.Matrix_MakeProjection(90.0F, height / width, 0.1F, 1000.0F);
            matTrans = Matrix.Matrix_MakeTranslation(0.0F, 0.0F, 5.0F);
            matWorld = Matrix.Matrix_MakeIdentity();
            matWorld = Matrix.Matrix_MultiplyMatrix(matWorld, matTrans);
        }
    }
}
