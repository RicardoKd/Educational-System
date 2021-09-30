using System.Drawing;

namespace Lab_2 {

    public class Line : Element {

        public Line(int x, int y, int d, int s) : base(x, y, d, s) {
        }

        public override void draw(Graphics G, Pen p) {
            if (direction == 1) {
                G.DrawLine(p, xpos, ypos, xpos + size, ypos);
            } else {
                G.DrawLine(p, xpos, ypos, xpos, ypos + size);
            }
        }
    }
}