using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class BezierVertex : IMovable
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public BezierVertex(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        public Point GetPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }
}
