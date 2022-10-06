using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class Relation
    {
        public RelationType Type { get; private set; }
        public (Vertex u, Vertex v) Pair { get; private set; }

        public Relation(Vertex u, Vertex v, RelationType rt)
        {
            Type = rt;
            Pair = (u, v);
        }
    }
}
