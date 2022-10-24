namespace Polygon
{
    public partial class InputDialog : Form
    {
        private string text;
        public InputDialog(string text)
        {
            InitializeComponent();
            inputTextBoxDialog.Text = text;
            this.text = text;
        }

        private void buttonDialogOK_Click(object sender, EventArgs e)
        {
            this.Text = inputTextBoxDialog.Text;
        }
    }
}
