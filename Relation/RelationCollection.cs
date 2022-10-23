using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    class RelationCollection
    {
        private Dictionary<Edge, List<Relation>> _relations;
        private Font _font;
        private Brush _brush;
        private int parallelRelations;

        public RelationCollection()
        {
            _relations = new Dictionary<Edge, List<Relation>>();
            _font = new Font("Arial", 10);
            _brush = new SolidBrush(Color.Blue);
            parallelRelations = 0;
        }

        public void AddSizeRelation(Edge e, double length)
        {

            if(_relations.Count(x => x.Key.Equals(e)) == 0)
            {
                _relations.Add(e, new List<Relation>());
            }

            // Add relation to list and to vertices
            e = _relations.First(x => x.Key.Equals(e)).Key;
            SizeRelation? rel = null;
            foreach (var relation in _relations[e])
            {
                if (relation is SizeRelation)
                {
                    rel = (SizeRelation)relation;
                    rel.UpdateLength(length);
                }
            }
            if(rel == null)
            {
                rel = new SizeRelation(e, length);
                _relations[e].Add(rel);
                e.U.AddRelation(rel);
                e.V.AddRelation(rel);
            }

        }

        public bool AddParallelRelation(Edge e1, Edge e2)
        {
            if (e1.Intersect(e2))
                return false;

            ParallelRelation rel = new ParallelRelation(e1, e2, parallelRelations++);
            rel.InitRelation();
            if (_relations.Count(x => x.Key.Equals(e1)) == 0)
            {
                _relations.Add(e1, new List<Relation>());
            }
            if (_relations.Count(x => x.Key.Equals(e2)) == 0)
            {
                _relations.Add(e2, new List<Relation>());
            }

            _relations[_relations.First(x => x.Key.Equals(e1)).Key].Add(rel);
            _relations[_relations.First(x => x.Key.Equals(e2)).Key].Add(rel);
            e1.U.AddRelation(rel);
            e1.V.AddRelation(rel);
            e2.U.AddRelation(rel);
            e2.V.AddRelation(rel);

            return true;
        }

        public void DeleteRelations(Edge e, string text)
        {
            foreach (var edge in _relations.Keys)
            {
                if (edge.Equals(e))
                {
                    var rel = _relations[edge].Find(x => x.GetName().Equals(text));
                    if (rel != null)
                    {
                        _relations[edge].Remove(rel);
                        rel.Dismantle();
                    }

                }
            }
            CorrectRelations();
        }

        public void DeleteRelations(Edge e)
        {
            foreach (var edge in _relations.Keys)
            {
                if (edge.Equals(e))
                {
                    foreach(var rel in _relations[edge])
                    {
                        rel.Dismantle();
                    }
                    _relations.Remove(edge);
                }
            }
            CorrectRelations();
        }

        public void DeleteRelations(Vertex v)
        {
            foreach(var edge in _relations.Keys)
            {
                if(edge.Contains(v))
                {
                    foreach(var rel in _relations[edge])
                    {
                        rel.Dismantle();
                    }
                    _relations.Remove(edge);
                }
            }
            CorrectRelations();
        }

        private void CorrectRelations()
        {
            foreach (var edge in _relations.Keys)
            {
                _relations[edge].RemoveAll(x => x.WasDismantled);
            }
        }

        public void DrawRelations(Graphics g)
        {
            foreach(var edge in _relations.Keys)
            {
                // draw icon in the middle of the edge
                double cx = (edge.U.X + edge.V.X) / 2;
                double cy = (edge.U.Y + edge.V.Y) / 2;

                StringBuilder icon = new StringBuilder();

                foreach(var rel in _relations[edge])
                {
                    icon.Append(rel.GetIcon());
                    icon.Append(" ");
                }

                //https://social.msdn.microsoft.com/Forums/windows/en-US/146f5b15-62a0-4b9e-ba22-7fcebd4df80b/drawstring-with-solid-background?forum=winforms

                string text = icon.ToString();

                Size size = TextRenderer.MeasureText(text, _font);
                Point p = new Point((int)(cx - size.Width / 2), (int)(cy - size.Height / 2));


                g.FillRectangle(Brushes.AntiqueWhite, new Rectangle(p, size));
                g.DrawString(text, _font, _brush, p);
            }
        }

        public List<Relation> GetRelation(Edge e)
        {
            foreach (var edge in _relations.Keys)
            {
                if (edge.Equals(e))
                {
                    return _relations[edge];
                }
            }
            return new List<Relation>();
        }
    }
}
