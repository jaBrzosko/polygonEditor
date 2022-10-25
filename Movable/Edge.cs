namespace Polygon
{
    // Edge is more of a container for two vertices. There is no guarantee two vertices will always be in the same Edge class
    internal class Edge : IMovable
    {
        public Vertex U { get; set; }
        public Vertex V { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((U.X - V.X) * (U.X - V.X) + (U.Y - V.Y) * (U.Y - V.Y));
            }
        }

        public Edge(Vertex u, Vertex v)
        {
            this.U = u;
            this.V = v;
        }

        public void Draw(Graphics g, SolidBrush brush, Pen pen, int radius, bool drawBersenham, Bitmap image)
        {
            if (drawBersenham)
            {
                LineDrawer.DrawBersenhamLine(image, U.GetPoint(), V.GetPoint(), brush.Color);
            }
            else
            {
                LineDrawer.DrawLine(g, pen, U.GetPoint(), V.GetPoint());
            }
        }

        public void Move(double dx, double dy)
        {
            // In the beggining edge was moved as two vertices. Finally it's done via proxy thorugh one of them
            // It let's us properly apply relations without double moving
            U.MoveByEdge(dx, dy, V);
        }

        public override bool Equals(object? obj) // Edge(u, v) == (Edge(v, u) and architecture flaws
        {
            if (obj == null || obj.GetType() != typeof(Edge))
            {
                return false;
            }

            Edge e = (Edge)obj;

            return (U == e.U && V == e.V) || (U == e.V && V == e.U);
        }

        public bool Contains(Vertex w)
        {
            return U == w || V == w;
        }

        public bool Intersect(Edge e) // both have at least one common vertex
        {
            return e.U == U || e.U == V || e.V == U || e.V == V;
        }
    }
}
