using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal abstract class Relation
    {
        public bool WasApplied { get; protected set; }

        public abstract void ApplyRelation(Vertex u, double dx, double dy);
        public abstract bool EdgeSetCheck(Vertex u, Vertex v);
        public abstract string GetIcon();
    }
}
