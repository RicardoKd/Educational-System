using System.Drawing;

namespace Lab_2 {

    internal class thingy : Element {

        public thingy(int x, int y, int d) : base(x, y, d, 1) {
        }

        public override void draw(Graphics G, Pen p) {
            if (direction == 1) {
                G.DrawLine(p, xpos, ypos - 27, xpos, ypos + 27);
                p.Width = 5;
                G.DrawLine(p, xpos + 8, ypos - 13, xpos + 8, ypos + 13);
            } else {
                G.DrawLine(p, xpos - 27, ypos, xpos + 27, ypos);
                p.Width = 5;
                G.DrawLine(p, xpos - 13, ypos + 8, xpos + 13, ypos + 8);
            }
            p.Width = 1;
        }
    }
}