using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class ParallelRelation : Relation
    {
        private Vertex u1, v1, u2, v2;

        public ParallelRelation(Edge e1, Edge e2) : this(e1.U, e1.V, e2.U, e2.V) { }

        public ParallelRelation(Vertex u1, Vertex v1, Vertex u2, Vertex v2)
        {
            // probably have to implement some smart way to sort them
            this.u1 = u1;
            this.v1 = v1;
            this.u2 = u2;
            this.v2 = v2;
        }
        public override void ApplyRelation(Vertex u, double dx, double dy)
        {
            if(u.Equals(u1))
            {
                AdjustEdge(u1, v1, u2, v2, dx, dy);
            }
            else if(u.Equals(u2))
            {
                AdjustEdge(u2 , v2, u1, v1, dx, dy);
            }
            else if(u.Equals(v1))
            {
                AdjustEdge(v1, u1, v2, u2, dx, dy);
            }
            else if(u.Equals(v2))
            {
                AdjustEdge(v2, u2, v1, u1, dx, dy);
            }
        }

        private void AdjustEdge(Vertex sourceMoved, Vertex sourceStayed, Vertex destinationToBeMoved, Vertex destinationStayed, double dx, double dy)
        {
            if (destinationStayed.X == destinationToBeMoved.X && destinationToBeMoved.Y == destinationStayed.Y)
                return;

            double nominator = GetDistanceSquared(destinationToBeMoved, destinationStayed);
            double denominator = GetDistanceSquared(sourceMoved, sourceStayed);
            if (denominator < 0.0001f)
                return;
            double coof = Math.Sqrt(nominator / denominator);
            destinationToBeMoved.Move(dx * coof, dy *coof);
        }

        private double GetDistanceSquared(Vertex u, Vertex v)
        {
            return (u.X - v.X) * (u.X - v.X) + (u.Y - v.Y) * (u.Y - v.Y);
        }

        private double GetDistance(Vertex u, Vertex v)
        {
            return Math.Sqrt(GetDistanceSquared(u, v));
        }

        public override bool EdgeSetCheck(Vertex u, Vertex v)
        {
            return (u.Equals(u1) && v.Equals(v1)) || (u.Equals(v1) && v.Equals(u1)) || (u.Equals(u2) && v.Equals(v2)) || (u.Equals(v2) && v.Equals(u2));
        }

        public void InitRelation()
        {
            // Maybe make a list and iterate over it?
            List<((double dx, double dy) delta, Vertex which)> temp = new List<((double dx, double dy) delta, Vertex which)>();
            temp.Add((PrecomputeCorrection(u1, v1, u2, v2), v2));
            temp.Add((PrecomputeCorrection(u1, v1, v2, u2), u2));
            temp.Add((PrecomputeCorrection(v1, u1, u2, v2), v2));
            temp.Add((PrecomputeCorrection(v1, u1, v2, u2), u2));
            temp.Add((PrecomputeCorrection(u2, v2, u1, v1), v1));
            temp.Add((PrecomputeCorrection(u2, v2, v1, u1), u1));
            temp.Add((PrecomputeCorrection(v2, u2, u1, v1), v1));
            temp.Add((PrecomputeCorrection(v2, u2, v1, u1), u1));

            double minDist = double.MaxValue;
            ((double dx, double dy) delta, Vertex which) result = temp[0];
            foreach(var it in temp)
            {
                var dist = it.delta.dx * it.delta.dx + it.delta.dy * it.delta.dy;
                if(dist < minDist)
                {
                    minDist = dist;
                    result = it;
                }
            }

            result.which.Move(result.delta.dx, result.delta.dy);
        }

        public (double dx, double dy) PrecomputeCorrection(Vertex u1, Vertex v1, Vertex u2, Vertex v2)
        {
            double dx = v1.X - u1.X;
            double dy = v1.Y - u1.Y;

            double coof = Math.Sqrt(GetDistanceSquared(u2, v2) / GetDistanceSquared(u1, v1));

            double nx = u2.X + dx * coof;
            double ny = u2.Y + dy * coof;

            double dnx = nx - v2.X;
            double dny = ny - v2.Y;

            return (dnx, dny);
        }
    }
}
