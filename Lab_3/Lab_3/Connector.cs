using System.Drawing;

namespace Lab_3 {

    internal class Connector : Element {

        public Connector(int x, int y, int s) : base(x, y, 0, s) {
        }

        public override void draw(Graphics G, Pen p) {
            G.DrawEllipse(p, xpos - (size / 2), ypos - (size / 2), size, size);
            G.FillEllipse(new SolidBrush(Color.Black), xpos - (size / 2), ypos - (size / 2), size, size);
        }
    }
}