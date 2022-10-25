namespace Polygon
{
    internal class Polygon : IMovable
    {
        private LinkedList<Vertex> vertices; 
        private List<Edge> edges;
        private readonly static double deltaE = 25; // squared - we don't have to calculate root
        private readonly static double deltaV = 25; // squared

        public Polygon()
        {
            vertices = new LinkedList<Vertex>();
            edges = new List<Edge>();
        }
        public Vertex LastVertex { get { return vertices.Last(); } }
        public int Count { get { return vertices.Count; } }

        public bool AddPoint(int x, int y)
        {
            Vertex temp = new Vertex(x, y);
            

            // we want to stop creating polygon when last vertex is first
            if (CheckIfEnds(x, y))
            {
                Edge tempEdge = new Edge(vertices.Last(), vertices.First());
                edges.Add(tempEdge);

                // this prevents creating line
                return vertices.Count > 2;
            }

            if (vertices.Count > 0)
            {
                Edge tempEdge = new Edge(vertices.Last(), temp);
                edges.Add(tempEdge);
            }

            vertices.AddLast(temp);
            return false;
        }

        // if point p corresponds to any movable part of this polygon we return it
        public IMovable? CheckMovable(Point p)
        {
            var v = CheckVertex(p);
            if (v != null)
                return v;
            var e = CheckEdge(p);
            if (e != null)
                return e;
            var poly = CheckPolygon(p);
            if (poly != null)
                return poly;

            return null;
        }

        public Vertex? CheckVertex(Point p)
        {
            foreach (var v in vertices)
            {
                if (CheckCollision(p.X, p.Y, v))
                    return v;
            }
            return null;
        }

        public Edge? CheckEdge(Point p)
        {
            var temp = new List<Vertex>();
            temp.AddRange(vertices);
            for (int i = 0; i < vertices.Count; i++)
            {
                int j = (i + 1) % vertices.Count;
                double result = GetDistance(p.X, p.Y, temp[i].X, temp[i].Y, temp[j].X, temp[j].Y);
                if (result < deltaE)
                {
                    foreach (var edge in edges)
                    {
                        if (edge.Contains(temp[i]) && edge.Contains(temp[j]))
                            return edge;
                    }
                }
            }
            return null;
        }

        // we check polygon as a square hitbox
        private Polygon? CheckPolygon(Point p)
        {
            double minx = vertices.Min(v => v.X);
            double maxx = vertices.Max(v => v.X);
            double miny = vertices.Min(v => v.Y);
            double maxy = vertices.Max(v => v.Y);

            if (p.X <= maxx && p.Y <= maxy && p.X >= minx && p.Y >= miny)
                return this;
            return null;
        }

        // distance from edge and not a line which is important
        private double GetDistance(double x0, double y0, double x1, double y1, double x2, double y2)
        {
            // https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment with slight modifications
            double A = x0 - x1;
            double B = y0 - y1;
            double C = x2 - x1;
            double D = y2 - y1;

            double dot = A * C + B * D;
            double len_sq = C * C + D * D;
            double param = -1;
            if (len_sq != 0)
                param = dot / len_sq;

            double xx, yy;

            if (param > 0 && param < 1)
            {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }
            else return double.MaxValue; // param in (0, 1) means we are in valid proximity of line

            var dx = x0 - xx;
            var dy = y0 - yy;
            return dx * dx + dy * dy;
        }

        private bool CheckCollision(int x, int y, Vertex v)
        {
            if (vertices.Count < 2)
            {
                return false;
            }
            return (v.X - x) * (v.X - x) + (v.Y - y) * (v.Y - y) < deltaV;
        }

        private bool CheckIfEnds(int x, int y)
        {
            if (vertices.Count == 0)
                return false;

            return CheckCollision(x, y, vertices.First());
        }

        public void DrawPolygon(Graphics g, SolidBrush brush, Pen pen, int radius, bool drawBersenham, Bitmap image)
        {
            foreach (var edge in edges)
            {
                edge.Draw(g, brush, pen, radius, drawBersenham, image);
            }
        }

        // the difference between this and DrawPolygon() is that we don't draw edge between first and last vertices
        public void DrawPolygonInCreation(Graphics g, SolidBrush brush, Pen pen, int radius, bool drawBersenham, Bitmap image)
        {
            foreach(var edge in edges)
            {
                edge.Draw(g, brush, pen, radius, drawBersenham, image);
            }
            g.FillEllipse(brush, (int)vertices.Last().X - radius, (int)vertices.Last().Y - radius, 2 * radius, 2 * radius);
        }

        public void Move(double dx, double dy)
        {
            foreach (var v in vertices)
            {
                v.MoveByPolygon(dx, dy);
            }
        }

        public void Insert(Edge edge, Point p)
        {
            var lln = vertices.Find(edge.U);
            Vertex newVertex = new Vertex(p.X, p.Y);
            if (lln == null)
                return;
            if ((lln.Next == null && vertices.First != null && vertices.First.Value.Equals(edge.V)) || (lln.Next != null && lln.Next.Value.Equals(edge.V)))
            {
                for(int i = 0; i < edges.Count; i++)
                {
                    if(edges[i].Equals(edge))
                    {
                        Edge newEdge = new Edge(newVertex, edge.V);
                        edges.Insert(i + 1, newEdge);
                        edge.V = newVertex;
                    }
                }
                vertices.AddAfter(lln, new LinkedListNode<Vertex>(newVertex));
                return;
            }
            lln = vertices.Find(edge.V);
            if (lln == null)
                return;
            
            for (int i = 0; i < edges.Count; i++)
            {
                if (edge.Equals(edge))
                {
                    Edge newEdge = new Edge(newVertex, edge.V);
                    edges.Insert(i + 1, newEdge);
                    edge.V = newVertex;
                    break;
                }
            }
            vertices.AddAfter(lln, new LinkedListNode<Vertex>(newVertex));
        }

        public bool Delete(Vertex v)
        {
            if (vertices.Count > 3)
            {
                var edgesWithV = edges.FindAll(e => e.Contains(v));
                if(edgesWithV.Count == 2)
                {
                    Vertex v1 = edgesWithV[0].U.Equals(v) ? edgesWithV[0].V : edgesWithV[0].U;
                    Vertex v2 = edgesWithV[1].U.Equals(v) ? edgesWithV[1].V : edgesWithV[1].U;
                    Edge newEdge = vertices.Last().Equals(v) || vertices.First().Equals(v) ? new Edge(v2, v1) : new Edge(v1, v2);
                    int index = edges.FindIndex(e => e.Equals(edgesWithV[0]));
                    edges.Insert(index, newEdge);
                    edges.Remove(edgesWithV[0]);
                    edges.Remove(edgesWithV[1]);
                }
                vertices.Remove(v);
                return false;
            }

            return true;
        }

        // when user decides to delete polygon it has to be dismantled so all relations are properly deleted
        public void Dismantle(RelationCollection relations)
        {
            foreach (var v in vertices)
            {
                relations.DeleteRelations(v);
            }
            vertices.Clear();
        }
    }
}
