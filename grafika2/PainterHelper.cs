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
            Point[] points = new Point[3];

            for (int i = 0; i < triangle.points.Length; i++)
            {
                int x = (int)Math.Round(triangle.points[i].x);
                int y = (int)Math.Round(triangle.points[i].y);

                Point point = new Point(x, y);

                points[i] = point;
            }

            using (Graphics gfx = Graphics.FromImage(bm))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            using(SolidBrush paintBrush = new SolidBrush(triangle.color))
            using (Pen pen = new Pen(brush))
            {

                gfx.FillPolygon(paintBrush, points);

                gfx.DrawLine(pen, triangle.points[0].x, triangle.points[0].y, triangle.points[1].x, triangle.points[1].y);
                gfx.DrawLine(pen, triangle.points[1].x, triangle.points[1].y, triangle.points[2].x, triangle.points[2].y);
                gfx.DrawLine(pen, triangle.points[2].x, triangle.points[2].y, triangle.points[0].x, triangle.points[0].y);

            };

        }
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public static void ClearScreen(Bitmap bm)
        {
            using (Graphics gfx = Graphics.FromImage(bm))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                gfx.FillRectangle(brush, 0, 0, bm.Width, bm.Height);
            };
        }
    }
}
