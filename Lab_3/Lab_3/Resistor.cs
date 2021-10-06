using System;
using System.Drawing;

namespace Lab_3 {

    internal class Resistor : Element {

        public Resistor(int x, int y, int d) : base(x, y, d, 1) {
        }

        public override void draw(Graphics G, Pen p) {
            if (direction == 1) { // horizontal
            } else {
                G.DrawLine(p, xpos, ypos, xpos, ypos + 15);
                G.DrawRectangle(p, xpos - 5, ypos + 15, 10, 40);
                G.DrawLine(p, xpos, ypos + 15 + 40, xpos, ypos + 55 + 15);
            }
        }
    }
}