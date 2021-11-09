using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mouse_Events_2 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            g = CreateGraphics();
        }

        private Graphics g;
        private Point selPoint;
        private Rectangle curR;
        private List<Rectangle> rectList = new List<Rectangle>();

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            selPoint = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Point p = e.Location;
                int x = Math.Min(selPoint.X, p.X);
                int y = Math.Min(selPoint.Y, p.Y);
                int w = Math.Abs(p.X - selPoint.X);
                int h = Math.Abs(p.Y - selPoint.Y);
                curR = new Rectangle(x, y, w, h);
                this.Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            rectList.Add(curR);
            SolidBrush greenBrush = new SolidBrush(Color.Green);

            foreach (Rectangle r in rectList) {
                g.DrawRectangle(Pens.Blue, r);
                g.FillRectangle(greenBrush, r);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.DrawRectangle(Pens.Blue, curR);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            foreach (Rectangle r in rectList) {
                g.DrawRectangle(Pens.Blue, r);
                g.FillRectangle(greenBrush, r);
            }
        }
    }
}