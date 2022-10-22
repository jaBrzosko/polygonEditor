using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class SizeRelation : Relation
    {
        private Vertex u;
        private Vertex v;

        public SizeRelation(Edge e) : this(e.U, e.V) { }
        public SizeRelation(Vertex u, Vertex v)
        {
            this.u = u;
            this.v = v;
        }
        public override void ApplyRelation(Vertex w, double dx, double dy)
        {
            WasApplied = true;
            if(u.Equals(w))
            {
                v.Move(dx, dy);
            }
            else
            {
                u.Move(dx, dy);
            }
            WasApplied = false;
        }
        public override bool EdgeSetCheck(Vertex U, Vertex V)
        {
            return (u.Equals(U) && v.Equals(V)) || (u.Equals(V) && v.Equals(U));
        }

        public override string GetIcon()
        {
            double dx = u.X - v.X;
            double dy = u.Y - v.Y;
            double length = Math.Sqrt(dx * dx + dy * dy);
            return length.ToString("0.##");
        }
    }
}
