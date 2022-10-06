using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class Vertex: IMovable
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point GetPoint()
        {
            return new Point((int)X, (int)Y);
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }
    }
}
