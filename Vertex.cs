namespace Polygon
{
    internal class Vertex : IMovable
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        private List<Relation> _relations;
        private bool wasMoved; // we don't move the same vertex twice in DFS relation search

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
            _relations = new List<Relation>();
            wasMoved = false;
        }

        public Point GetPoint()
        {
            return new Point((int)X, (int)Y);
        }


        // if user is moving whole polygon there is no need to check relations
        public void MoveByPolygon(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        public void Move(double dx, double dy)
        {
            if (wasMoved)
                return;
            wasMoved = true; // prevents recursion
            X += dx;
            Y += dy;

            foreach (var rel in _relations)
            {
                if (rel.WasApplied) // we don't want to calculate relation twice
                    continue;
                rel.ApplyRelation(this, dx, dy);
            }
            wasMoved = false;
        }

        // Moving edge as two separate vertices wasn't working because it was moving edge twice
        public void MoveByEdge(double dx, double dy, Vertex v)
        {
            wasMoved = true;
            v.wasMoved = true;

            X += dx;
            Y += dy;
            v.X += dx;
            v.Y += dy;
            foreach (var rel in _relations.Where(x => !x.WasApplied && !x.EdgeSetCheck(this, v)))
            {
                rel.ApplyRelation(this, dx, dy);
            }
            foreach (var rel in v._relations.Where(x => !x.WasApplied && !x.EdgeSetCheck(this, v)))
            {
                rel.ApplyRelation(v, dx, dy);
            }
            wasMoved = false;
            v.wasMoved = false;
        }

        public void AddRelation(Relation rel)
        {
            _relations.Add(rel);
        }

        public void DeleteRelation(Relation rel)
        {
            _relations.Remove(rel);
        }
    }
}
