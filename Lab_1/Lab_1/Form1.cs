using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_1 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            Graphics g = CreateGraphics();
            g.Clear(BackColor);

            Pen blackPen = new Pen(Color.Black, 3);
            Pen greenPen = new Pen(Color.Green, 2);

            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);

            Rectangle lgRect = new Rectangle(50, 70, 150, 250);
            Rectangle mdRect = new Rectangle(125, 120, 75, 200);
            Rectangle smRect = new Rectangle(125, 70, 75, 50);

            g.DrawRectangle(blackPen, lgRect);
            g.FillRectangle(blueBrush, lgRect);
            g.DrawRectangle(greenPen, smRect);
            g.FillRectangle(redBrush, smRect);
            g.DrawRectangle(greenPen, mdRect);
            g.FillRectangle(greenBrush, mdRect);

            Rectangle ovalRect = new Rectangle(275, 95, 150, 50);
            Rectangle ovalRect2 = new Rectangle(275, 270, 150, 50);
            Rectangle rect = new Rectangle(275, 120, 150, 175);

            g.DrawEllipse(greenPen, ovalRect2);
            g.FillEllipse(greenBrush, ovalRect2);
            g.DrawRectangle(greenPen, rect);
            g.FillRectangle(greenBrush, rect);
            g.DrawEllipse(greenPen, ovalRect);
            g.FillEllipse(redBrush, ovalRect);

            g.DrawLine(blackPen, 50, 350, 50, 400);
            Rectangle rRect = new Rectangle(50, 350, 30, 30);
            g.DrawArc(blackPen, rRect, 200, 320);

            g.DrawLine(blackPen, 100, 350, 100, 400);
            g.DrawLine(blackPen, 100, 400, 125, 350);
            g.DrawLine(blackPen, 125, 350, 125, 400);

            g.DrawLine(blackPen, 150, 350, 150, 375);
            g.DrawLine(blackPen, 150, 375, 170, 375);
            g.DrawLine(blackPen, 170, 350, 170, 400);

            g.DrawLine(blackPen, 195, 400, 210, 350);
            g.DrawLine(blackPen, 210, 350, 225, 400);
            g.DrawLine(blackPen, 203, 375, 217, 375);

            g.DrawLine(blackPen, 250, 350, 250, 400);
            Rectangle RRect = new Rectangle(250, 350, 30, 30);
            g.DrawArc(blackPen, RRect, 200, 320);

            g.DrawLine(blackPen, 300, 390, 315, 350);
            g.DrawLine(blackPen, 315, 350, 330, 390);
            g.DrawLine(blackPen, 290, 390, 340, 390);
            g.DrawLine(blackPen, 290, 390, 300, 400);
            g.DrawLine(blackPen, 340, 390, 330, 400);
        }
    }
}