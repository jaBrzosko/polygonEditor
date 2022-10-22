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
            //U.Move(dx, dy);
            //V.Move(dx, dy);
            // In the beggining edge was moved as two vertices. Finally it's done via proxy thorugh one of them
            // It let's us properly apply relations without double moving
            U.MoveByEdge(dx, dy, V);
        }

        public void Shrink(Vertex u, double length)
        {
            //TODO: Implement
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || obj.GetType() != typeof(Edge))
            {
                return false;
            }

            Edge e = (Edge)obj;

            return (U  == e.U && V == e.V) || (U == e.V && V == e.U);
        }
    }
}
