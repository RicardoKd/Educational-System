using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class StudentMainMenu : Form {
        private Form1 form1;
        public Student Student { get; set; }

        public StudentMainMenu(Student student, Form1 form1) {
            InitializeComponent();
            this.form1 = form1;
            Student = student;
        }

        private void StudentMainMenu_Load(object sender, EventArgs e) {
            label1.Text = Student.Group;
            label2.Text = "Hello, " + Student.Username;
            List<string> grSubjList = Rules.getSubjList(Student.Group);
            List<Button> btnList = Services.createBtnList(80, 150, grSubjList, DynamicButton_Click);
            foreach (Button btn in btnList)
                Controls.Add(btn);
        }

        private void DynamicButton_Click(object sender, EventArgs e) {
            string subject = (sender as Button).Text;
            SubjectTasksStudent STS = new SubjectTasksStudent(subject, this);
            STS.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e) {
            form1.Show();
            Close();
        }
    }
}