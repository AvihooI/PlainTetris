using System.Windows.Forms;

namespace PlainTetris
{
    class CanvasForm : Form
    {
        public CanvasForm()
        {
            Width = 500;
            Height = 640;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
    }
}