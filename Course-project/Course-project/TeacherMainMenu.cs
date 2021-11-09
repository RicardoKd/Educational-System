using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class TeacherMainMenu : Form {
        private Form1 form1;

        public Teacher Teacher { get; set; }

        public TeacherMainMenu(Teacher teacher, Form1 form1) {
            InitializeComponent();
            Teacher = teacher;
            this.form1 = form1;
        }

        private void TeacherMainMenu_Load(object sender, EventArgs e) {
            label1.Text = Teacher.Subject;
            label2.Text = "Hello, " + Teacher.Username;
            List<string> grListSort = Services.getGroupsWithSubj(Teacher.Subject);
            List<Button> btnList = Services.createBtnList(20, 150, grListSort, DynamicButton_Click);
            foreach (Button btn in btnList)
                Controls.Add(btn);
        }

        private void DynamicButton_Click(object sender, EventArgs e) {
            string grName = (sender as Button).Text; // get text of the btn that was clicked
            GroupInfo grInfo = new GroupInfo(grName, this);
            grInfo.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e) {
        }

        private void button2_Click(object sender, EventArgs e) {
            form1.Show();
            Close();
        }
    }
}