using System.Windows.Forms;

namespace PlainTetris
{
    class CanvasForm : Form
    {
        public CanvasForm()
        {
            Width = 580;
            Height = 680;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
        }
    }
}