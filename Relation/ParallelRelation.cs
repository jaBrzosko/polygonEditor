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
        private int relationNumber;

        public ParallelRelation(Edge e1, Edge e2, int relationNumber = 0) : this(e1.U, e1.V, e2.U, e2.V, relationNumber) { }

        public ParallelRelation(Vertex u1, Vertex v1, Vertex u2, Vertex v2, int relationNumber)
        {
            // probably have to implement some smart way to sort them
            this.u1 = u1;
            this.v1 = v1;
            this.u2 = u2;
            this.v2 = v2;
            this.relationNumber = relationNumber;
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
            var eq1 = SolveEquation(sourceMoved, sourceStayed);
            var eq2 = SolveEquation(destinationToBeMoved, destinationStayed);

            var neq = SolveForNewValues(eq1, eq2);

            AdjustVertex(neq.A, neq.B, destinationToBeMoved, destinationStayed);


            //if (destinationStayed.X == destinationToBeMoved.X && destinationToBeMoved.Y == destinationStayed.Y)
            //    return;

            //double nominator = GetDistanceSquared(destinationToBeMoved, destinationStayed);
            //double denominator = GetDistanceSquared(sourceMoved, sourceStayed);
            //if (denominator < 0.0001f)
            //    return;
            //double coof = Math.Sqrt(nominator / denominator);
            //destinationToBeMoved.Move(dx * coof, dy *coof);
        }

        private (double A, double B) SolveEquation(Vertex u, Vertex v)
        {
            double A, B;
            if(u.Y == v.Y)
            {
                A = 0;
                B = 1;
            }
            else if (u.X == v.X)
            {
                A = 1;
                B = 0;
            }
            else
            {
                // z kart maturalnych CKE, strona 7, "Równanie prostej, która przechodzi przez dwa dane punkty"
                // https://cke.gov.pl/images/_EGZAMIN_MATURALNY_OD_2015/Informatory/2015/MATURA_2015_Wybrane_wzory_matematyczne.pdf
                A = u.Y - v.Y;
                B = v.X - u.X;
            }

            return (A, B);
        }

        private (double A, double B) SolveForNewValues((double A, double B) eq1, (double A, double B) eq2)
        {
            // also thanks to CKE
            if (eq1.A == 0 || eq1.B == 0)
                return (eq1.A, eq1.B);
            return (eq1.A * eq2.B / eq1.B, eq2.B);
        }

        private void AdjustVertex(double A, double B, Vertex toBeMoved, Vertex stationary)
        {
            double dx = toBeMoved.X - stationary.X;
            double dy = toBeMoved.Y - stationary.Y;

            double denom = B * B + A * A;
            double ab = A * B;

            double nx = (B * B * dx - ab * dy) / denom;
            double ny = (A * A * dy - ab * dx) / denom;

            double dnx = nx - dx;
            double dny = ny - dy;

            toBeMoved.Move(dnx, dny);
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

        public override string GetIcon()
        {
            return "||  " + relationNumber.ToString();
        }
    }
}
