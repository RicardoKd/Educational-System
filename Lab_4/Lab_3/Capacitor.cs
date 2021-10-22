using System;
using System.Drawing;

namespace Lab_3 {

    internal class Capacitor : Element {

        public Capacitor(int x, int y, int d) : base(x, y, d, 1) {
        }

        public override void draw(Graphics G, Pen p) {
            if (direction == 1) {
                G.DrawLine(p, xpos, ypos - 15, xpos, ypos + 15);
                G.DrawLine(p, xpos + 5, ypos - 15, xpos + 5, ypos + 15);
            } else {
                G.DrawLine(p, xpos - 15, ypos, xpos + 15, ypos);
                G.DrawLine(p, xpos - 15, ypos + 5, xpos + 15, ypos + 5);
            }
        }
    }
}