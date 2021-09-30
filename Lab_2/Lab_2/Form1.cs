using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_2 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Graphics G = CreateGraphics();
            Pen black = new Pen(Color.Black);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            int X0 = 100, Y0 = 100;
            Element[] el = new Element[20];
            el[0] = new Line(X0, Y0, 0, 80);
            el[1] = new Line(X0, Y0 + 80, 0, 35); // Switch
            el[2] = new Line(X0, Y0 + 115, 0, 115);
            el[3] = new Line(X0, Y0 + 230, 1, 50);
            el[4] = new Lamp(X0 + 50, Y0 + 230, 1, 60);
            el[5] = new Line(X0 + 110, Y0 + 230, 1, 100);
            el[6] = new Lamp(X0 + 210, Y0 + 230, 1, 60);
            el[7] = new Line(X0 + 270, Y0 + 230, 1, 50);
            el[8] = new Line(X0 + 320, Y0, 0, 230);
            el[9] = new Line(X0 + 320, Y0, 1, -125);
            el[10] = new Line(X0, Y0, 1, 120);
            el[11] = new thingy(X0 + 120, Y0, 1);
            el[12] = new Line(X0 + 120 + 8, Y0, 1, 60);
            el[13] = new thingy(X0 + 120 + 8 + 60, Y0, 1);
            el[14] = new Point(X0, Y0 + 80, 10);
            el[15] = new Point(X0, Y0 + 115, 10);

            for (int i = 0; i < 16; i++)
                el[i].draw(G, black);
        }
    }
}