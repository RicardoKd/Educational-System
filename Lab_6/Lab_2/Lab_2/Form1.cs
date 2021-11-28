using System.Drawing;
using System.Windows.Forms;

namespace Lab_2 {

    public partial class Form1 : Form {
        private Graphics g;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            g = CreateGraphics();
            if (e.Button == MouseButtons.Left) {
                Pen p = new Pen(Brushes.Purple, 3);
                SolidBrush sb = new SolidBrush(Color.LightGray);
                Point[] pointArr = {
                    new Point(e.X, e.Y - 40),
                    new Point(e.X - 34, e.Y + 20),
                    new Point(e.X + 34, e.Y + 20),
                };
                g.DrawPolygon(p, pointArr);
                g.FillPolygon(sb, pointArr);
                p.Dispose();
            }
        }
    }
}