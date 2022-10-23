namespace Polygon
{
    partial class InputDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonDialogOK = new System.Windows.Forms.Button();
            this.buttonDialogCancel = new System.Windows.Forms.Button();
            this.inputTextBoxDialog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDialogOK
            // 
            this.buttonDialogOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDialogOK.Location = new System.Drawing.Point(12, 85);
            this.buttonDialogOK.Name = "buttonDialogOK";
            this.buttonDialogOK.Size = new System.Drawing.Size(136, 23);
            this.buttonDialogOK.TabIndex = 0;
            this.buttonDialogOK.Text = "OK";
            this.buttonDialogOK.UseVisualStyleBackColor = true;
            this.buttonDialogOK.Click += new System.EventHandler(this.buttonDialogOK_Click);
            // 
            // buttonDialogCancel
            // 
            this.buttonDialogCancel.Location = new System.Drawing.Point(228, 85);
            this.buttonDialogCancel.Name = "buttonDialogCancel";
            this.buttonDialogCancel.Size = new System.Drawing.Size(130, 23);
            this.buttonDialogCancel.TabIndex = 1;
            this.buttonDialogCancel.Text = "Cancel";
            this.buttonDialogCancel.UseVisualStyleBackColor = true;
            // 
            // inputTextBoxDialog
            // 
            this.inputTextBoxDialog.Location = new System.Drawing.Point(85, 25);
            this.inputTextBoxDialog.Name = "inputTextBoxDialog";
            this.inputTextBoxDialog.Size = new System.Drawing.Size(218, 23);
            this.inputTextBoxDialog.TabIndex = 2;
            // 
            // InputDialog
            // 
            this.AcceptButton = this.buttonDialogOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.buttonDialogCancel;
            this.ClientSize = new System.Drawing.Size(370, 115);
            this.Controls.Add(this.inputTextBoxDialog);
            this.Controls.Add(this.buttonDialogCancel);
            this.Controls.Add(this.buttonDialogOK);
            this.Name = "InputDialog";
            this.Text = "InputDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonDialogOK;
        private Button buttonDialogCancel;
        public TextBox inputTextBoxDialog;
    }
}