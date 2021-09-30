using System.Drawing;

namespace Lab_2 {

    public class Point : Element {

        public Point(int x, int y, int s) : base(x, y, 0, s) {
        }

        public override void draw(Graphics G, Pen p) {
            G.DrawEllipse(p, xpos - (size / 2), ypos - (size / 2), size, size);
            G.FillEllipse(new SolidBrush(Color.Black), xpos - (size / 2), ypos - (size / 2), size, size);
            /*if (direction == 1) {
                G.FillEllipse(new SolidBrush(Color.Black), xpos - 2, ypos - 2, 4, 4);
            } else {
                G.FillEllipse(new SolidBrush(Color.Black), xpos - 2, ypos - 2, 4, 4);
            }*/
        }
    }
}