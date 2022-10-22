using System.Diagnostics;

namespace Polygon
{
    public partial class Form1 : Form
    {

        private List<Polygon> polygons;
        private Polygon? creating;
        private Bitmap background;
        private WorkType workType;
        private IMovable? movable;
        private Point? LastPosition;
        private Edge? firstEdge;
        private bool drawBresenham;
        private RelationCollection relations;

        private readonly Color backgroundColor = Color.Gray;
        private readonly Color drawColor = Color.Black;
        private readonly int radius = 4;
        public Form1()
        {
            InitializeComponent();

            creating = null;
            polygons = new List<Polygon>();
            background = new Bitmap(canvas.Width, canvas.Height);
            canvas.Image = background;
            firstEdge = null;
            drawBresenham = false;
            relations = new RelationCollection();
            Redraw();
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            switch(workType)
            {
                case WorkType.Create:
                    CreateMode(e);
                    break;
                case WorkType.Edit:
                    EditMode(e);
                    break;
            }
        }

        private void CreateMode(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (creating == null)
                {
                    creating = new Polygon();
                }

                if (creating.AddPoint(e.X, e.Y))
                {
                    polygons.Add(creating);
                    creating = null;
                }
                Redraw();
            }
        }

        private void EditMode(MouseEventArgs e)
        {
            //implement drag - not used currently
        }

        private void Redraw()
        {
            Redraw(null);
        }

        private void Redraw(MouseEventArgs? e)
        {
            using Graphics g = Graphics.FromImage(background);
            g.Clear(backgroundColor);

            using SolidBrush brush = new SolidBrush(drawColor);
            using Pen pen = new Pen(drawColor, 2);

            foreach (Polygon polygon in polygons)
            {
                polygon.DrawPolygon(g, brush, pen, radius, drawBresenham, background);
            }

            if(creating != null)
            {
                creating.DrawPolygonInCreation(g, brush, pen, radius, drawBresenham, background);
                if (e != null)
                {
                    if (drawBresenham)
                    {
                        LineDrawer.DrawBersenhamLine(background, creating.LastVertex.GetPoint(), e.Location);
                    }
                    else
                    {
                        LineDrawer.DrawLine(g, pen, creating.LastVertex.GetPoint(), e.Location);
                    }
                }
            }

            relations.DrawRelations(g);

            canvas.Refresh();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch(workType)
            {
                case WorkType.Create:
                    Redraw(e);
                    break;
                case WorkType.Edit:
                    MoveVertex(e);
                    break;
            }
        }

        private void MoveVertex(MouseEventArgs e)
        {

            if (movable == null || LastPosition == null)
                return;

            // delta from last check
            movable.Move(e.X - LastPosition.Value.X, e.Y - LastPosition.Value.Y);
            Redraw();
            LastPosition = e.Location;
        }

        private void radioButtonCreate_CheckedChanged(object sender, EventArgs e)
        {
            workType = WorkType.Create;
        }

        private void radioButtonEdit_CheckedChanged(object sender, EventArgs e)
        {
            workType = WorkType.Edit;
            creating = null;
            Redraw();
        }

        private void radioButtonRelations_CheckedChanged(object sender, EventArgs e)
        {
            workType = WorkType.Relations;
            creating = null;
            Redraw();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(workType == WorkType.Edit && e.Button == MouseButtons.Left)
            {
                movable = CheckVertex(e);
                LastPosition = e.Location;
            }
        }

        private IMovable? CheckVertex(MouseEventArgs e)
        {
            foreach(Polygon polygon in polygons)
            {
                var temp = polygon.CheckMovable(e.Location);
                if (temp != null)
                {
                    return temp;
                }
            }
            return null;
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (workType == WorkType.Edit)
            {
                movable = null;
                LastPosition = null;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Size size = tableLayoutPanel.GetControlFromPosition(1, 0).Size;
            Bitmap newBitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.Clear(backgroundColor);
            }
            canvas.Image = newBitmap;
            background.Dispose();
            background = newBitmap;
            Redraw();
        }

        private void canvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch(workType)
            {
                case WorkType.Edit:
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            InsertNewVertex(e);
                            break;
                        case MouseButtons.Right:
                            DeleteVertex(e);
                            break;
                    }
                    break;
                case WorkType.Relations:
                    switch(e.Button)
                    {
                        case MouseButtons.Left:
                            AddSizeRelation(e);
                            break;
                        case MouseButtons.Right:
                            AddParallelRelation(e);
                            break;
                    }
                    break;
            }

        }

        private void AddSizeRelation(MouseEventArgs e)
        {
            foreach(var polygon in polygons)
            {
                var edge = polygon.CheckEdge(e.Location);
                if (edge == null)
                    continue;

                relations.AddSizeRelation(edge);

                //SizeRelation rel = new SizeRelation(edge);
                //edge.U.AddRelation(rel);
                //edge.V.AddRelation(rel);
            }
            Redraw();
        }

        private void AddParallelRelation(MouseEventArgs e)
        {
            foreach (var polygon in polygons)
            {
                var edge = polygon.CheckEdge(e.Location);
                if (edge == null)
                    continue;
                if(firstEdge == null)
                {
                    firstEdge = edge;
                    return;
                }

                // dont allow relations that are neighbours
                relations.AddParallelRelation(firstEdge, edge);

                //ParallelRelation parallelRelation = new ParallelRelation(firstEdge, edge);
                //parallelRelation.InitRelation();
                //firstEdge.U.AddRelation(parallelRelation);
                //firstEdge.V.AddRelation(parallelRelation);
                //edge.U.AddRelation(parallelRelation);
                //edge.V.AddRelation(parallelRelation);
                firstEdge = null;
            }
            Redraw();
        }

        private void InsertNewVertex(MouseEventArgs e)
        {
            foreach (var polygon in polygons)
            {
                var edge = polygon.CheckEdge(e.Location);
                if (edge == null)
                    continue;
                polygon.Insert(edge, e.Location);
                Redraw();
                return;
            }
        }

        private void DeleteVertex(MouseEventArgs e)
        {
            foreach(var polygon in polygons)
            {
                var v = polygon.CheckVertex(e.Location);
                if (v == null)
                    continue;
                if(polygon.Delete(v))
                {
                    polygons.Remove(polygon);
                }

                Redraw();
                return;
            }
        }

        private void radioButtonLineLibrary_CheckedChanged(object sender, EventArgs e)
        {
            drawBresenham = false;
            Redraw();
        }

        private void radioButtonLineBrensenham_CheckedChanged(object sender, EventArgs e)
        {
            drawBresenham = true;
            Redraw();
        }
    }
}