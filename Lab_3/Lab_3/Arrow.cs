using System.Drawing;

namespace Lab_3 {

    internal class Arrow : Element {

        public Arrow(int x, int y, int d, int s) : base(x, y, d, s) {
        }

        public override void draw(Graphics G, Pen p) {
            if (direction == 1) {
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                Point a = new Point(xpos, ypos - 8);
                Point b = new Point(xpos + size, ypos);
                Point c = new Point(xpos, ypos + 8);
                Point[] curvePoints = { a, b, c };
                G.FillPolygon(blackBrush, curvePoints);
            } else {
            }
        }
    }
}