using System;
using System.Windows.Forms;

namespace Course_project {

    public partial class StudentMainMenu : Form {
        private Form1 form1;

        public StudentMainMenu(Student student, Form1 form1) {
            InitializeComponent();
            this.form1 = form1;
        }

        private void StudentMainMenu_Load(object sender, EventArgs e) {
        }

        private void button2_Click(object sender, EventArgs e) {
            form1.Show();
            Close();
        }
    }
}