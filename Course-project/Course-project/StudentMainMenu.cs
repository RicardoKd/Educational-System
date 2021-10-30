using System;
using System.Windows.Forms;

namespace Course_project {

    public partial class StudentMainMenu : Form {
        private Form1 form1;

        public StudentMainMenu(User user, Form1 form1) {
            InitializeComponent();
            this.form1 = form1;
        }

        private void StudentMainMenu_Load(object sender, EventArgs e) {
        }
    }
}