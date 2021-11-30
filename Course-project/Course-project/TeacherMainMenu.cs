using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class TeacherMainMenu : Form {
        public Form1 Form1 { get; set; }
        public Teacher Teacher { get; set; }

        public TeacherMainMenu(Teacher teacher, Form1 form1) {
            InitializeComponent();
            Teacher = teacher;
            Form1 = form1;
        }

        private void TeacherMainMenu_Load(object sender, EventArgs e) {
            label1.Text = Teacher.Subject;
            label2.Text = "Hello, " + Teacher.Username;
            List<string> grListSort = Services.getGroupsWithSubj(Teacher.Subject);
            List<Button> btnList = Services.createBtnList(20, 150, grListSort, dynamicBtn_Click);
            foreach (Button btn in btnList)
                Controls.Add(btn);
        }

        private void dynamicBtn_Click(object sender, EventArgs e) {
            string grName = (sender as Button).Text; // get text of the btn that was clicked
            GroupInfo grInfo = new GroupInfo(grName, this);
            grInfo.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e) {
            Form1.Show();
            Close();
        }
    }
}