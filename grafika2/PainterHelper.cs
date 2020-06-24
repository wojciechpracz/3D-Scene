using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2
{
    static class PainterHelper
    {
        public static void DrawTriangle(Triangle triangle, Bitmap bm)
        {
            using (Graphics gfx = Graphics.FromImage(bm))
            using (SolidBrush brush = new SolidBrush(Color.White))
            using (Pen pen = new Pen(brush))
            {
                gfx.DrawLine(pen, triangle.points[0].x, triangle.points[0].y, triangle.points[1].x, triangle.points[1].y);
                gfx.DrawLine(pen, triangle.points[1].x, triangle.points[1].y, triangle.points[2].x, triangle.points[2].y);
                gfx.DrawLine(pen, triangle.points[2].x, triangle.points[2].y, triangle.points[0].x, triangle.points[0].y);
            };

        }
    }
}
