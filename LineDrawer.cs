namespace Polygon
{
    internal class LineDrawer
    {
        public static void DrawBezier(Graphics g, Pen pen, Point v0, Point v1, Point v2, Point v3)
        {

            //draw help lines
            Pen dashPen = new Pen(Color.GreenYellow, 2);
            dashPen.DashPattern = new float[] { 8.0F, 4.0F, 2.0F, 6.0F };

            g.DrawLine(dashPen, v0, v1);
            g.DrawLine(dashPen, v1, v2);
            g.DrawLine(dashPen, v2, v3);

            MyDrawBezier(g, pen, v0, v1, v2, v3);
        }

        public static void MyDrawBezier(Graphics g, Pen p, Point v0, Point v1, Point v2, Point v3)
        {
            double t0 = 0, t1 = 1;
            double step = 0.01;
            (double X, double Y) A0 = (v0.X, v0.Y);
            (double X, double Y) A1 = (3 * (v1.X - v0.X), 3 * (v1.Y - v0.Y));
            (double X, double Y) A2 = (3 * (v2.X - 2 * v1.X + v0.X), 3 * (v2.Y - 2 * v1.Y + v0.Y));
            (double X, double Y) A3 = (v3.X - 3 * v2.X + 3 * v1.X - v0.X, v3.Y - 3 * v2.Y + 3 * v1.Y - v0.Y);
            Point prev = v0;
            for(double t = t0; t <= t1; t+=step)
            {
                double nx = t * (t * (t * A3.X + A2.X) + A1.X) + A0.X;
                double ny = t * (t * (t * A3.Y + A2.Y) + A1.Y) + A0.Y;
                Point newPoint = new Point((int)nx, (int)ny);
                g.DrawLine(p, prev, newPoint);
                prev = newPoint;
            }
            g.DrawLine(p, prev, v3);
        }

        public static void DrawLine(Graphics g, Pen pen, Point u, Point v)
        {
            g.DrawLine(pen, u, v);
        }

        //https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
        public static void DrawBersenhamLine(Bitmap image, Point u, Point v, Color color)
        {
            int x0 = u.X;
            int y0 = u.Y;
            int x1 = v.X;
            int y1 = v.Y;
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                    PlotLineLow(x1, y1, x0, y0, image, color);
                else
                    PlotLineLow(x0, y0, x1, y1, image, color);

            }
            else
            {
                if (y0 > y1)
                    PlotLineHigh(x1, y1, x0, y0, image, color);
                else
                    PlotLineHigh(x0, y0, x1, y1, image, color);
            }
        }

        private static void PlotLineLow(int x0, int y0, int x1, int y1, Bitmap image, Color color)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            int D = (2 * dy) - dx;
            int y = y0;

            for (int x = x0; x < x1; x++)
            {
                if (x > 0 && y > 0 && x < image.Width && y < image.Height) // this if is required by how C# Bitmap works
                    image.SetPixel(x, y, color);
                if (D > 0)
                {
                    y += yi;
                    D += 2 * (dy - dx);
                }
                else
                {
                    D += 2 * dy;
                }
            }
        }

        private static void PlotLineHigh(int x0, int y0, int x1, int y1, Bitmap image, Color color)
        {
            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }
            int D = (2 * dx) - dy;
            int x = x0;

            for (int y = y0; y < y1; y++)
            {
                if (x > 0 && y > 0 && x < image.Width && y < image.Height)
                    image.SetPixel(x, y, color);
                if (D > 0)
                {
                    x += xi;
                    D += 2 * (dx - dy);
                }
                else
                {
                    D += 2 * dx;
                }
            }
        }
    }
}
