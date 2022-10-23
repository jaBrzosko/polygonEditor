using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon
{
    internal class SceneLoader
    {

        public static void LoadScene(List<Polygon> polygons, RelationCollection relations)
        {
            // Polygon 1
            Polygon pol1 = new Polygon();

            Point p11 = new Point(110, 80);
            Point p12 = new Point(250, 200);
            Point p13 = new Point(170, 200);

            Point[] points1 = new Point[] { p11, p12, p13 };

            foreach(var point in points1)
            {
                pol1.AddPoint(point.X, point.Y);
            }


            polygons.Add(pol1);

            // Polygon 2
            Polygon pol2 = new Polygon();

            Point p21 = new Point(400, 200);
            Point p22 = new Point(450, 300);
            Point p23 = new Point(650, 300);
            Point p24 = new Point(600, 200);

            Point[] points2 = new Point[] { p21, p22, p23, p24 };

            foreach (var point in points2)
            {
                pol2.AddPoint(point.X, point.Y);
            }


            polygons.Add(pol2);

            // Polygon 3
            Polygon pol3 = new Polygon();

            Point p31 = new Point(100, 300);
            Point p32 = new Point(80, 450);
            Point p33 = new Point(180, 550);
            Point p34 = new Point(400, 480);
            Point p35 = new Point(250, 480);
            Point p36 = new Point(350, 420);
            Point p37 = new Point(210, 390);
            Point p38 = new Point(140, 280);

            Point[] points3 = new Point[] { p31, p32, p33, p34, p35, p36, p37, p38};

            foreach (var point in points3)
            {
                pol3.AddPoint(point.X, point.Y);
            }


            polygons.Add(pol3);

            // Size relations
            {
                Edge? e1 = pol3.CheckEdge(new Point((p34.X + p35.X) / 2, (p34.Y + p35.Y) / 2));
                Edge? e2 = pol1.CheckEdge(new Point((p12.X + p13.X) / 2, (p12.Y + p13.Y) / 2));

                Edge? e3 = pol3.CheckEdge(new Point((p31.X + p32.X) / 2, (p31.Y + p32.Y) / 2));




                if (e1 != null && e2 != null)
                {
                    relations.AddSizeRelation(e1, e1.Length);
                    relations.AddParallelRelation(e1, e2);
                }
                if (e3 != null)
                    relations.AddSizeRelation(e3, e3.Length + 20);
            }

            // Parallel relations
            {
                Edge? e1 = pol2.CheckEdge(new Point((p21.X + p22.X) / 2, (p21.Y + p22.Y) / 2));
                Edge? e2 = pol2.CheckEdge(new Point((p23.X + p24.X) / 2, (p23.Y + p24.Y) / 2));

                Edge? e3 = pol2.CheckEdge(new Point((p21.X + p24.X) / 2, (p21.Y + p24.Y) / 2));
                Edge? e4 = pol2.CheckEdge(new Point((p23.X + p22.X) / 2, (p23.Y + p22.Y) / 2));

                Edge? e5 = pol3.CheckEdge(new Point((p37.X + p38.X) / 2, (p37.Y + p38.Y) / 2));
                Edge? e6 = pol1.CheckEdge(new Point((p11.X + p13.X) / 2, (p11.Y + p13.Y) / 2));


                if (e1 != null && e2 != null)
                    relations.AddParallelRelation(e1, e2);

                if (e3 != null && e4 != null)
                    relations.AddParallelRelation(e3, e4);

                if (e5 != null && e6 != null)
                    relations.AddParallelRelation(e5, e6);
            }

        }

    }
}
