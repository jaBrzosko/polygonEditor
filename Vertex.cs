using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class Vertex: IMovable
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public List<Relation> Relations { get; private set; }

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
            Relations = new List<Relation>();
        }

        // returns true if was able to add relation - not possible to make adjacent edges parallel
        public bool AddRelation(Relation relation, bool isU)
        {
            //implement checking if parallel is possible
            Relations.Add(relation);

            return true;
        }

        public void DeleteRelation(Relation relation)
        {
            Relations.Remove(relation);
        }

        public Point GetPoint()
        {
            return new Point((int)X, (int)Y);
        }

        public void Move(double dx, double dy)
        {
            BlankMove(dx, dy);
            CalculateRelations(dx, dy);
        }

        public void MoveForced(double dx, double dy)
        {
            BlankMove(dx, dy);
        }

        private void BlankMove(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        private void CalculateRelations(double dx, double dy)
        {
            foreach(var rel in Relations)
            {
                if(rel.Type == RelationType.Size)
                {
                    if(rel.Pair.u.Equals(this))
                    {
                        rel.Pair.v.BlankMove(dx, dy);
                    }
                    else if(rel.Pair.v.Equals(this))
                    {
                        rel.Pair.u.BlankMove(dx, dy);
                    }
                }
            }
        }
    }
}
