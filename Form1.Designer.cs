﻿namespace Polygon
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEdit = new System.Windows.Forms.RadioButton();
            this.radioButtonCreate = new System.Windows.Forms.RadioButton();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.radioButtonRelations = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
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
            this.groupBox1.Controls.Add(this.radioButtonRelations);
            this.groupBox1.Controls.Add(this.radioButtonEdit);
            this.groupBox1.Controls.Add(this.radioButtonCreate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 100);
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
            this.canvas.Location = new System.Drawing.Point(127, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(650, 424);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseClick);
            this.canvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDoubleClick);
            this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
            this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
            this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
            // 
            // radioButtonRelations
            // 
            this.radioButtonRelations.AutoSize = true;
            this.radioButtonRelations.Location = new System.Drawing.Point(15, 72);
            this.radioButtonRelations.Name = "radioButtonRelations";
            this.radioButtonRelations.Size = new System.Drawing.Size(73, 19);
            this.radioButtonRelations.TabIndex = 2;
            this.radioButtonRelations.TabStop = true;
            this.radioButtonRelations.Text = "Relations";
            this.radioButtonRelations.UseVisualStyleBackColor = true;
            this.radioButtonRelations.CheckedChanged += new System.EventHandler(this.radioButtonRelations_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tableLayoutPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private GroupBox groupBox1;
        private RadioButton radioButtonEdit;
        private RadioButton radioButtonCreate;
        private PictureBox canvas;
        private RadioButton radioButtonRelations;
    }
}