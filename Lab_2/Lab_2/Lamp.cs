using System;
using System.Drawing;

namespace Lab_2 {

    public class Lamp : Element {

        public Lamp(int x, int y, int d, int s) : base(x, y, d, s) {
        }

        public override void draw(Graphics G, Pen p) {
            float difference = Convert.ToSingle(Math.Sqrt(2 * Math.Pow(size / 2, 2)) / 2);

            if (direction == 1) {
                int x = xpos, y = ypos - size / 2;
                G.DrawEllipse(p, x, y, size, size);
                int x0 = x + size / 2;
                G.DrawLine(p, x0 - difference, ypos - difference, x0 + difference, ypos + difference);
                G.DrawLine(p, x0 - difference, ypos + difference, x0 + difference, ypos - difference);
            } else {
                int x = xpos - size / 2, y = ypos;
                G.DrawEllipse(p, x, y, size, size); int y0 = y + size / 2;
                G.DrawLine(p, xpos - difference, y0 - difference, xpos + difference, y0 + difference);
                G.DrawLine(p, xpos - difference, y0 + difference, xpos + difference, y0 - difference);
            }
        }
    }
}