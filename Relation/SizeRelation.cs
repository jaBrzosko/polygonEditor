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
        private double length;

        public SizeRelation(Edge e, double length) : this(e.U, e.V, length) { }
        public SizeRelation(Vertex u, Vertex v, double length)
        {
            this.u = u;
            this.v = v;
            this.length = length;
            Shrink(u, v);
        }

        private void Shrink(Vertex p, Vertex q) //p was moved, q is to be corrected
        {
            // vector from q to p
            double dx = p.X - q.X;
            double dy = p.Y - q.Y;

            // distance of edge at this point
            double distance = Math.Sqrt(dx * dx + dy * dy);

            double tdx = (distance - length) / distance * dx;
            double tdy = (distance - length) / distance * dy;

            q.Move(tdx, tdy);
        }


        public override void ApplyRelation(Vertex w, double dx, double dy)
        {
            WasApplied = true;
            if(u.Equals(w))
            {
                Shrink(u, v);
                //v.Move(dx, dy);
            }
            else
            {
                Shrink(v, u);
                //u.Move(dx, dy);
            }
            WasApplied = false;
        }

        public void UpdateLength(double length)
        {
            this.length = length;
            Shrink(u, v);
        }

        public override bool EdgeSetCheck(Vertex U, Vertex V)
        {
            return (u.Equals(U) && v.Equals(V)) || (u.Equals(V) && v.Equals(U));
        }

        public override string GetIcon()
        {
            // we calculate it everytime in case something doesn't scale properly
            double dx = u.X - v.X;
            double dy = u.Y - v.Y;
            double length = Math.Sqrt(dx * dx + dy * dy);
            return length.ToString("F2");
        }

        public override void Dismantle()
        {
            u.DeleteRelation(this);
            v.DeleteRelation(this);
            WasDismantled = true;
        }

        public override string GetName()
        {
            return "Size relation";
        }
    }
}
