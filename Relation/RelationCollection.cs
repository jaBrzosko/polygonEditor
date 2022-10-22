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

        public void AddSizeRelation(Edge e)
        {
            SizeRelation rel = new SizeRelation(e);
            // refine it to set only one relation
            if(!_relations.ContainsKey(e))
            {
                _relations.Add(e, new List<Relation>());
            }

            // Add relation to list and to vertices
            _relations[e].Add(rel);
            e.U.AddRelation(rel);
            e.V.AddRelation(rel);
        }

        public void AddParallelRelation(Edge e1, Edge e2)
        {
            ParallelRelation rel = new ParallelRelation(e1, e2, parallelRelations++);
            rel.InitRelation();
            if (!_relations.ContainsKey(e1))
            {
                _relations.Add(e1, new List<Relation>());
            }
            if (!_relations.ContainsKey(e2))
            {
                _relations.Add(e2, new List<Relation>());
            }

            _relations[e1].Add(rel);
            _relations[e2].Add(rel);
            e1.U.AddRelation(rel);
            e1.V.AddRelation(rel);
            e2.U.AddRelation(rel);
            e2.V.AddRelation(rel);
        }

        public void DeleteRelations(Edge e)
        {

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

                g.DrawString(icon.ToString(), _font, _brush, new Point((int)cx, (int)cy));
            }
        }
    }
}
