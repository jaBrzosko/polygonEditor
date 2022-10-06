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

        private readonly Color backgroundColor = Color.White;
        private readonly Color drawColor = Color.Black;
        private readonly int radius = 4;
        public Form1()
        {
            InitializeComponent();

            creating = null;
            polygons = new List<Polygon>();
            background = new Bitmap(canvas.Width, canvas.Height);
            canvas.Image = background;
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
                polygon.DrawPolygon(g, brush, pen, radius);
            }

            if(creating != null)
            {
                creating.DrawPolygonInCreation(g, brush, pen, radius);
                if (e != null)
                {
                    g.DrawLine(pen, creating.LastVertex.GetPoint(), e.Location);
                }
            }

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
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            InsertSizeRelation(e);
                            break;
                    }
                    break;
            }
            
        }

        private void InsertSizeRelation(MouseEventArgs e)
        {
            foreach (var polygon in polygons)
            {
                var edge = polygon.CheckEdge(e.Location);
                if (edge == null)
                    continue;
                Relation temp = new Relation(edge.U, edge.V, RelationType.Size);
                edge.U.AddRelation(temp, true);
                edge.V.AddRelation(temp, false);
                Redraw();
                return;
            }
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
                polygon.Delete(v);
                Redraw();
                return;
            }
        }

    }
}