using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_1 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
            Text = "Hey graphical world!";
            Width = 300;
            Height = 400;
            BackColor = Color.Cyan;
            MouseClick += HandlerClass.Form1_MouseClick;
            KeyDown += HandlerClass.CtrlDownHandler;
        }

        private void Form1_Load(object sender, EventArgs e) {
            Left = 40;
            Top = 100;
        }
    }
}