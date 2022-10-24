namespace Polygon
{
    internal class ParallelRelation : Relation
    {
        private Vertex u1, v1, u2, v2;
        private int relationNumber; // it helps to tell relations apart for user

        public ParallelRelation(Edge e1, Edge e2, int relationNumber = 0) : this(e1.U, e1.V, e2.U, e2.V, relationNumber) { }

        public ParallelRelation(Vertex u1, Vertex v1, Vertex u2, Vertex v2, int relationNumber)
        {
            this.u1 = u1;
            this.v1 = v1;
            this.u2 = u2;
            this.v2 = v2;
            this.relationNumber = relationNumber;
        }

        public override void ApplyRelation(Vertex u, double dx, double dy)
        {
            // we calculate which vertex should be corrected
            if (u.Equals(u1))
            {
                AdjustEdge(u1, v1, u2, v2, dx, dy);
            }
            else if (u.Equals(u2))
            {
                AdjustEdge(u2, v2, u1, v1, dx, dy);
            }
            else if (u.Equals(v1))
            {
                AdjustEdge(v1, u1, v2, u2, dx, dy);
            }
            else if (u.Equals(v2))
            {
                AdjustEdge(v2, u2, v1, u1, dx, dy);
            }
        }

        // we solve Ax + By + C = 0 equations for both edges and then correct the proper one to match parallel property
        private void AdjustEdge(Vertex sourceMoved, Vertex sourceStayed, Vertex destinationToBeMoved, Vertex destinationStayed, double dx, double dy)
        {
            var eq1 = SolveEquation(sourceMoved, sourceStayed);
            var eq2 = SolveEquation(destinationToBeMoved, destinationStayed);

            var neq = SolveForNewValues(eq1, eq2);

            AdjustVertex(neq.A, neq.B, destinationToBeMoved, destinationStayed);
        }

        private (double A, double B) SolveEquation(Vertex u, Vertex v)
        {
            double A, B;
            if (u.Y == v.Y)
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

        // it calculates orthogonal projection of point onto line Ax + BY that goes through stationary Vertex
        private void AdjustVertex(double A, double B, Vertex toBeMoved, Vertex stationary)
        {
            // vector from stationary to toBeMoved
            double dx = toBeMoved.X - stationary.X;
            double dy = toBeMoved.Y - stationary.Y;

            // parameter calculations which were determined outside code


            double dnx, dny;
            //check if A or B is 0
            // A == 0 => By + C = 0, line is vertical
            if(B == 0)
            {
                dnx = 0;
                dny = stationary.Y - toBeMoved.Y;
            }
            // B == 0 => Ax + C = 0, line is horizontal
            else if(B == 0)
            {
                dnx = stationary.X - toBeMoved.X;
                dny = 0;
            }
            else
            {
                double denom = B * B + A * A;
                double ab = A * B;
                // new x and y values - orthogonal projection
                double nx = (B * B * dx - ab * dy) / denom;
                double ny = (A * A * dy - ab * dx) / denom;

                // vector values by which toBeMoved should be moved
                dnx = nx - dx;
                dny = ny - dy;
            }


            toBeMoved.Move(dnx, dny);
        }

        private double GetDistanceSquared(Vertex u, Vertex v)
        {
            return (u.X - v.X) * (u.X - v.X) + (u.Y - v.Y) * (u.Y - v.Y);
        }

        public override bool EdgeSetCheck(Vertex u, Vertex v)
        {
            return (u.Equals(u1) && v.Equals(v1)) || (u.Equals(v1) && v.Equals(u1)) || (u.Equals(u2) && v.Equals(v2)) || (u.Equals(v2) && v.Equals(u2));
        }

        public void InitRelation()
        {
            // we make list of all possible moves so that we pick the best one - it can be done on init, but
            // rather slows program too much resources in real time calculations
            List<((double dx, double dy) delta, Vertex which)> temp = new List<((double dx, double dy) delta, Vertex which)>();
            temp.Add((PrecomputeCorrection(u1, v1, u2, v2), v2));
            temp.Add((PrecomputeCorrection(u1, v1, v2, u2), u2));
            temp.Add((PrecomputeCorrection(v1, u1, u2, v2), v2));
            temp.Add((PrecomputeCorrection(v1, u1, v2, u2), u2));
            temp.Add((PrecomputeCorrection(u2, v2, u1, v1), v1));
            temp.Add((PrecomputeCorrection(u2, v2, v1, u1), u1));
            temp.Add((PrecomputeCorrection(v2, u2, u1, v1), v1));
            temp.Add((PrecomputeCorrection(v2, u2, v1, u1), u1));

            // we find the best move
            double minDist = double.MaxValue;
            ((double dx, double dy) delta, Vertex which) result = temp[0];
            foreach (var it in temp)
            {
                var dist = it.delta.dx * it.delta.dx + it.delta.dy * it.delta.dy;
                if (dist < minDist)
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

        public override void Dismantle()
        {
            u1.DeleteRelation(this);
            v1.DeleteRelation(this);
            u2.DeleteRelation(this);
            v2.DeleteRelation(this);
            WasDismantled = true;
        }

        public override string GetName()
        {
            return "Parallel relation number " + relationNumber;
        }
    }
}
