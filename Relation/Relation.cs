namespace Polygon
{
    internal abstract class Relation
    {
        public bool WasApplied { get; protected set; }
        public bool WasDismantled { get; protected set; }

        protected Relation()
        {
            WasDismantled = false;
        }

        public abstract void ApplyRelation(Vertex u, double dx, double dy);
        public abstract bool EdgeSetCheck(Vertex u, Vertex v);
        public abstract string GetIcon(); // is used in drawing relation
        public abstract string GetName(); // is used in context menu
        public abstract void Dismantle();
    }
}
