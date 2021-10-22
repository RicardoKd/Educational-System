using System;
using System.Drawing;

namespace Lab_3 {

    internal class Inductor : Element {

        public Inductor(int x, int y, int d) : base(x, y, d, 1) {
        }

        public override void draw(Graphics G, Pen p) {
            if (direction == 1) { // horizontal
                G.DrawLine(p, xpos, ypos, xpos + 11, ypos);
                G.DrawArc(p, xpos + 11, ypos - 8, 15, 30, -45, -90);
                G.DrawArc(p, xpos + 25, ypos - 8, 15, 30, -45, -90);
                G.DrawArc(p, xpos + 39, ypos - 8, 15, 30, -45, -90);
                G.DrawLine(p, xpos + 54, ypos, xpos + 65, ypos);
            } else {
                G.DrawLine(p, xpos, ypos, xpos, ypos + 11);
                G.DrawArc(p, xpos - 22, ypos + 11, 30, 15, -45, 90);
                G.DrawArc(p, xpos - 22, ypos + 25, 30, 15, -45, 90);
                G.DrawArc(p, xpos - 22, ypos + 39, 30, 15, -45, 90);
                G.DrawLine(p, xpos, ypos + 54, xpos, ypos + 65);
            }
        }
    }
}