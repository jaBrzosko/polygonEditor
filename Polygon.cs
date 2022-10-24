﻿namespace Polygon
{
    internal class Polygon : IMovable
    {
        private LinkedList<Vertex> vertices; 
        private readonly static double deltaE = 25; // squared - we don't have to calculate root
        private readonly static double deltaV = 25; // squared

        public Polygon()
        {
            vertices = new LinkedList<Vertex>();
        }
        public Vertex LastVertex { get { return vertices.Last(); } }
        public int Count { get { return vertices.Count; } }

        public bool AddPoint(int x, int y)
        {
            Vertex temp = new Vertex(x, y);

            // we want to stop creating polygon when last vertex is first
            if (CheckIfEnds(x, y))
            {
                // this prevents creating line
                return vertices.Count > 2;
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
                    return new Edge(temp[i], temp[j]);
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
            Point prev = vertices.Last().GetPoint();
            foreach (Vertex v in vertices)
            {
                Point point = v.GetPoint();
                g.FillEllipse(brush, point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
                if (drawBersenham)
                {
                    LineDrawer.DrawBersenhamLine(image, prev, point, brush.Color);
                }
                else
                {
                    LineDrawer.DrawLine(g, pen, point, prev);
                }
                prev = point;
            }
        }

        // the difference between this and DrawPolygon() is that we don't draw edge between first and last vertices
        public void DrawPolygonInCreation(Graphics g, SolidBrush brush, Pen pen, int radius, bool drawBersenham, Bitmap image)
        {
            Point prev = vertices.First().GetPoint();
            g.FillEllipse(brush, prev.X - radius, prev.Y - radius, 2 * radius, 2 * radius);
            foreach (Vertex v in vertices.Skip(1))
            {
                Point point = v.GetPoint();
                g.FillEllipse(brush, point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
                if (drawBersenham)
                {
                    LineDrawer.DrawBersenhamLine(image, prev, point, brush.Color);
                }
                else
                {
                    LineDrawer.DrawLine(g, pen, point, prev);
                }
                prev = point;
            }
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
            if (lln == null)
                return;
            if ((lln.Next == null && vertices.First != null && vertices.First.Value.Equals(edge.V)) || (lln.Next != null && lln.Next.Value.Equals(edge.V)))
            {
                vertices.AddAfter(lln, new LinkedListNode<Vertex>(new Vertex(p.X, p.Y)));
                return;
            }
            lln = vertices.Find(edge.V);
            if (lln == null)
                return;
            vertices.AddAfter(lln, new LinkedListNode<Vertex>(new Vertex(p.X, p.Y)));
        }

        public bool Delete(Vertex v)
        {
            if (vertices.Count > 3)
            {
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
