﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class LineDrawer
    {
        public static void DrawLine(Graphics g, Pen pen, Point u, Point v)
        {
            g.DrawLine(pen, u, v);
        }

        //https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
        public static void DrawBersenhamLine(Bitmap image, Point u, Point v)
        {
            int x0 = u.X;
            int y0 = u.Y;
            int x1 = v.X;
            int y1 = v.Y;
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                    PlotLineLow(x1, y1, x0, y0, image);
                else
                    PlotLineLow(x0, y0, x1, y1, image);

            }
            else
            {
                if (y0 > y1)
                    PlotLineHigh(x1, y1, x0, y0, image);
                else
                    PlotLineHigh(x0, y0, x1, y1, image);
            }
        }

        private static void PlotLineLow(int x0, int y0, int x1, int y1, Bitmap image)
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
                if(x > 0 && y > 0 && x < image.Width && y < image.Height)
                    image.SetPixel(x, y, Color.Black);
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

        private static void PlotLineHigh(int x0, int y0, int x1, int y1, Bitmap image)
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
                    image.SetPixel(x, y, Color.Black);
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
