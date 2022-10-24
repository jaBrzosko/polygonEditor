namespace Polygon
{
    public partial class Form1 : Form
    {

        private List<Polygon> polygons;
        private Polygon? creating;
        private Bitmap background;
        private WorkType workType;
        private IMovable? movable;
        private IMovable? clickable;
        private Point? LastPosition;
        private Point? ContextMenuPosition;
        private Edge? firstEdge;
        private bool drawBresenham;
        private bool isCreating;
        private RelationCollection relations;
        private Edge? contextEdge;

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
            isCreating = false;
            contextEdge = null;
            relations = new RelationCollection();
            workType = WorkType.Edit;

            SceneLoader.LoadScene(polygons, relations);
            Redraw();
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            switch (workType)
            {
                case WorkType.Create:
                    CreateMode(e);
                    break;
                case WorkType.Edit:
                    if (firstEdge != null)
                        AddParallelRelation(e.Location);
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
                    if (isCreating)
                    {
                        PolygonButton.PerformClick();
                    }
                }
                Redraw();
            }
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

            if (creating != null)
            {
                creating.DrawPolygonInCreation(g, brush, pen, radius, drawBresenham, background);
                if (e != null)
                {
                    if (drawBresenham)
                    {
                        LineDrawer.DrawBersenhamLine(background, creating.LastVertex.GetPoint(), e.Location, brush.Color);
                    }
                    else
                    {
                        LineDrawer.DrawLine(g, pen, creating.LastVertex.GetPoint(), e.Location);
                    }
                }
            }

            if (firstEdge != null)
            {
                if (drawBresenham)
                {
                    LineDrawer.DrawBersenhamLine(background, firstEdge.U.GetPoint(), firstEdge.V.GetPoint(), Color.Orange);
                }
                else
                {
                    using Pen pen1 = new Pen(Color.Orange, 2);
                    LineDrawer.DrawLine(g, pen1, firstEdge.U.GetPoint(), firstEdge.V.GetPoint());
                }
            }

            relations.DrawRelations(g);

            canvas.Refresh();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch (workType)
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

        private void PolygonButton_Click(object sender, EventArgs e)
        {
            // switch button for creating polygons
            if (isCreating)
            {
                workType = WorkType.Edit;
                creating = null;
                PolygonButton.Text = "Add polygon";
            }
            else
            {
                workType = WorkType.Create;
                PolygonButton.Text = "Cancel";
            }
            isCreating = !isCreating;
            Redraw();
        }

        // we start recording drag and drop
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (workType == WorkType.Edit && e.Button == MouseButtons.Left)
            {
                movable = CheckMovable(e);
                LastPosition = e.Location;
            }
        }

        // check if IMovable object was clicked
        private IMovable? CheckMovable(MouseEventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                var temp = polygon.CheckMovable(e.Location);
                if (temp != null)
                {
                    return temp;
                }
            }
            return null;
        }

        // end of drag and drop mechanic
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (workType == WorkType.Edit)
            {
                movable = null;
                LastPosition = null;
            }
        }

        // scale up and down bitmap
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

        private void AddSizeRelation(Point p)
        {

            foreach (var polygon in polygons)
            {
                var edge = polygon.CheckEdge(p);
                if (edge == null)
                    continue;

                Form dialog = new InputDialog(edge.Length.ToString("F2"));
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;

                double lenght;
                try
                {
                    lenght = double.Parse(dialog.Text);
                }
                catch
                {
                    // NaN was inputed
                    MessageBox.Show("Inputed value isn't proper lenght", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                relations.AddSizeRelation(edge, lenght);
            }
            Redraw();
        }

        private void AddParallelRelation(Point p)
        {
            foreach (var polygon in polygons)
            {
                var edge = polygon.CheckEdge(p);
                if (edge == null)
                    continue;
                if (firstEdge == null)
                {
                    firstEdge = edge;
                    Redraw();
                    return;
                }

                if (!relations.AddParallelRelation(firstEdge, edge))
                {
                    MessageBox.Show("You can't make neighboring edges parallel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                firstEdge = null;
            }
            Redraw();
        }

        private void InsertNewVertex(Point p)
        {
            foreach (var polygon in polygons)
            {
                var edge = polygon.CheckEdge(p);
                if (edge == null)
                    continue;
                relations.DeleteRelations(edge);
                polygon.Insert(edge, p);
                Redraw();
                return;
            }
        }

        private void DeleteVertex(Point p)
        {
            foreach (var polygon in polygons)
            {
                var v = polygon.CheckVertex(p);
                if (v == null)
                    continue;

                relations.DeleteRelations(v);
                if (polygon.Delete(v))
                {
                    // if returned value is true then polygon is triangle - smallest polygon
                    var result = MessageBox.Show("Are you sure you want to delete whole polygon?", "Polygon delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result != DialogResult.OK)
                        return;

                    polygon.Dismantle(relations);
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

        private void addSizeRelationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContextMenuPosition == null)
                return;
            AddSizeRelation((Point)ContextMenuPosition);
            ContextMenuPosition = null;
        }

        private void addParallelRelationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContextMenuPosition == null)
                return;
            AddParallelRelation((Point)ContextMenuPosition);
            ContextMenuPosition = null;
        }

        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContextMenuPosition == null)
                return;
            InsertNewVertex((Point)ContextMenuPosition);
            ContextMenuPosition = null;
        }

        private void removeVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContextMenuPosition == null)
                return;
            DeleteVertex((Point)ContextMenuPosition);
            ContextMenuPosition = null;
        }

        private void contextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // reset context menu items
            foreach (ToolStripItem item in contextMenu.Items)
            {
                item.Enabled = false;
            }
            contextEdge = null;
            removeRelationToolStripMenuItem.DropDownItems.Clear();
        }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (workType != WorkType.Edit)
                return;

            //if we are not creating polygon enable proper context menu items
            ContextMenuPosition = canvas.PointToClient(Cursor.Position);
            foreach (var polygon in polygons)
            {

                // check if vertex was clicked
                IMovable? temp = polygon.CheckVertex((Point)ContextMenuPosition);
                if (temp != null)
                {
                    clickable = temp;
                    removeVertexToolStripMenuItem.Enabled = true;
                    return;
                }

                //check if edge was clicked
                temp = polygon.CheckEdge((Point)ContextMenuPosition);
                if (temp != null)
                {
                    clickable = temp;
                    addSizeRelationToolStripMenuItem.Enabled = true;
                    if (firstEdge == null)
                    {
                        addParallelRelationToolStripMenuItem.Enabled = true;
                    }
                    addVertexToolStripMenuItem.Enabled = true;

                    // check for all relations on the edge
                    var rels = relations.GetRelation((Edge)temp);

                    if (rels.Count > 0)
                    {
                        removeRelationToolStripMenuItem.Enabled = true;
                        contextEdge = (Edge)temp;

                        foreach (var rel in rels)
                        {
                            removeRelationToolStripMenuItem.DropDownItems.Add(rel.GetName());
                        }
                    }
                    return;
                }

            }
            ContextMenuPosition = null;
        }

        private void removeRelationToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (contextEdge == null)
                return;
            relations.DeleteRelations(contextEdge, e.ClickedItem.Text);
            Redraw();
        }
    }
}