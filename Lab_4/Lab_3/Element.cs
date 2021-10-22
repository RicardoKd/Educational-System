using System.Drawing;

namespace Lab_3 {

    public abstract class Element {
        protected int xpos;
        protected int ypos;
        protected int direction;
        protected int size;

        public Element(int x, int y, int d, int s) {
            xpos = x;
            ypos = y;
            if (d == 0)
                direction = 0;
            else
                direction = 1;
            size = s;
        }

        public abstract void draw(Graphics G, Pen p);
    }
}