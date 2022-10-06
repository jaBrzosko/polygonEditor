using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class Edge: IMovable
    {
        public Vertex U { get; private set; }
        public Vertex V { get; private set; }

        public Edge(Vertex u, Vertex v)
        {
            this.U = u;
            this.V = v;
        }

        public void Move(double dx, double dy)
        {
            U.MoveForced(dx, dy);
            V.MoveForced(dx, dy);
        }
    }
}
