using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class StudentMainMenu : Form {
        public Form1 Form1 { get; set; }
        public Student Student { get; set; }

        public StudentMainMenu(Student student, Form1 form1) {
            InitializeComponent();
            Form1 = form1;
            Student = student;
        }

        private void StudentMainMenu_Load(object sender, EventArgs e) {
            label1.Text = Student.Group;
            label2.Text = "Hello, " + Student.Username;
            List<string> grSubjList = new List<string>(Rules.getSubjList(Student.Group));
            List<Button> btnList = Services.createBtnList(115, 150, grSubjList, DynamicButton_Click);
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
            Form1.Show();
            Close();
        }
    }
}