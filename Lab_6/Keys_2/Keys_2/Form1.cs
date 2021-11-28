using System;
using System.Windows.Forms;

namespace Keys_2 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            string date = DateTime.Now.ToString();

            if (e.Shift) {
                label1.Text += "\n" + date + "    Key: Shift";
            } else if (e.Control) {
                label1.Text += "\n" + date + "    Key: Ctrl";
            } else if (e.Alt) {
                label1.Text += "\n" + date + "    Key: Alt";
            } else if (e.Modifiers == Keys.Shift && e.Shift) {
                label1.Text += "\n" + date + "    Combination: " + e.KeyData.ToString();
            } else if (e.Modifiers == Keys.Control && !e.Control) {
                label1.Text += "\n" + date + "    Combination: " + e.KeyData.ToString();
            } else if (e.Modifiers == Keys.Alt && !e.Alt) {
                label1.Text += "\n" + date + "    Combination: " + e.KeyData.ToString();
            } else {
                label1.Text += "\n" + date + "    Key: " + e.KeyData.ToString();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
        }
    }
}