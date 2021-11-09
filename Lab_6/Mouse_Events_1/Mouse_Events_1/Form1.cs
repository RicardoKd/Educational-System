using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Mouse_Events_1 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            bPaint = false;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e) {
            richTextBox1.Text = "pic 1 enter\n" + richTextBox1.Text;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e) {
            richTextBox1.Text = "pic 2 enter\n" + richTextBox1.Text;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e) {
            richTextBox1.Text = "pic 1 leave\n" + richTextBox1.Text;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e) {
            richTextBox1.Text = "pic 2 leave\n" + richTextBox1.Text;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {
        }
    }
}