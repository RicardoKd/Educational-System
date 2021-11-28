using System.Windows.Forms;

namespace Lab_1 {

    internal class HandlerClass {

        public static void Form1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
                (sender as Form).Top += 50;
        }

        public static void CtrlDownHandler(object sender, KeyEventArgs e) {
            if (e.Control)
                (sender as Form).Top += 50;
        }
    }
}