namespace Polygon
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEdit = new System.Windows.Forms.RadioButton();
            this.radioButtonCreate = new System.Windows.Forms.RadioButton();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSizeRelationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addParallelRelationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.97938F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.02062F));
            this.tableLayoutPanel.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.canvas, 1, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(780, 430);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonEdit);
            this.groupBox1.Controls.Add(this.radioButtonCreate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // radioButtonEdit
            // 
            this.radioButtonEdit.AutoSize = true;
            this.radioButtonEdit.Location = new System.Drawing.Point(15, 47);
            this.radioButtonEdit.Name = "radioButtonEdit";
            this.radioButtonEdit.Size = new System.Drawing.Size(45, 19);
            this.radioButtonEdit.TabIndex = 1;
            this.radioButtonEdit.TabStop = true;
            this.radioButtonEdit.Text = "Edit";
            this.radioButtonEdit.UseVisualStyleBackColor = true;
            this.radioButtonEdit.CheckedChanged += new System.EventHandler(this.radioButtonEdit_CheckedChanged);
            // 
            // radioButtonCreate
            // 
            this.radioButtonCreate.AutoSize = true;
            this.radioButtonCreate.Location = new System.Drawing.Point(15, 22);
            this.radioButtonCreate.Name = "radioButtonCreate";
            this.radioButtonCreate.Size = new System.Drawing.Size(59, 19);
            this.radioButtonCreate.TabIndex = 0;
            this.radioButtonCreate.TabStop = true;
            this.radioButtonCreate.Text = "Create";
            this.radioButtonCreate.UseVisualStyleBackColor = true;
            this.radioButtonCreate.CheckedChanged += new System.EventHandler(this.radioButtonCreate_CheckedChanged);
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.SystemColors.Control;
            this.canvas.ContextMenuStrip = this.contextMenu;
            this.canvas.Location = new System.Drawing.Point(127, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(650, 424);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseClick);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSizeRelationToolStripMenuItem,
            this.addParallelRelationToolStripMenuItem,
            this.addVertexToolStripMenuItem,
            this.removeVertexToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(181, 92);
            this.contextMenu.Text = "Control";
            this.contextMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenu_Closed);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // addSizeRelationToolStripMenuItem
            // 
            this.addSizeRelationToolStripMenuItem.Enabled = false;
            this.addSizeRelationToolStripMenuItem.Name = "addSizeRelationToolStripMenuItem";
            this.addSizeRelationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addSizeRelationToolStripMenuItem.Text = "Add size relation";
            this.addSizeRelationToolStripMenuItem.Click += new System.EventHandler(this.addSizeRelationToolStripMenuItem_Click);
            // 
            // addParallelRelationToolStripMenuItem
            // 
            this.addParallelRelationToolStripMenuItem.Enabled = false;
            this.addParallelRelationToolStripMenuItem.Name = "addParallelRelationToolStripMenuItem";
            this.addParallelRelationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addParallelRelationToolStripMenuItem.Text = "Add parallel relation";
            this.addParallelRelationToolStripMenuItem.Click += new System.EventHandler(this.addParallelRelationToolStripMenuItem_Click);
            // 
            // addVertexToolStripMenuItem
            // 
            this.addVertexToolStripMenuItem.Enabled = false;
            this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
            this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addVertexToolStripMenuItem.Text = "Add vertex";
            this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
            // 
            // removeVertexToolStripMenuItem
            // 
            this.removeVertexToolStripMenuItem.Enabled = false;
            this.removeVertexToolStripMenuItem.Name = "removeVertexToolStripMenuItem";
            this.removeVertexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeVertexToolStripMenuItem.Text = "Remove vertex";
            this.removeVertexToolStripMenuItem.Click += new System.EventHandler(this.removeVertexToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(13, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 72);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Line Drawing";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 47);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(84, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Bresenham";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButtonLineBrensenham_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(61, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Library";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButtonLineLibrary_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "Form1";
            this.Text = "Polygon Editor";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tableLayoutPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private GroupBox groupBox1;
        private RadioButton radioButtonEdit;
        private RadioButton radioButtonCreate;
        private PictureBox canvas;
        private GroupBox groupBox2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem addSizeRelationToolStripMenuItem;
        private ToolStripMenuItem addParallelRelationToolStripMenuItem;
        private ToolStripMenuItem addVertexToolStripMenuItem;
        private ToolStripMenuItem removeVertexToolStripMenuItem;
    }
}