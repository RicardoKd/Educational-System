using System;
using System.Globalization;
using System.Windows.Forms;

namespace Course_project {

    public partial class SubjectTasksStudent : Form {
        public string LectDir { get; set; } // with semester
        public string Subject { get; set; }
        public string TestDir { get; set; } // with semester
        public StudentMainMenu StudentMainMenu { get; set; }

        public SubjectTasksStudent(string subject, StudentMainMenu studentMainMenu) {
            InitializeComponent();
            StudentMainMenu = studentMainMenu;
            Subject = subject;
            Group group = new Group(StudentMainMenu.Student.Group);
            int semester = Services.GetCurrentSemester();
            LectDir = "Lectures/" + group.Specialty + "/" + group.Year + "/" + Subject + "/" + semester + "/";
            TestDir = "Tests/" + group.Specialty + "/" + group.Year + "/" + Subject + "/" + semester + "/";
        }

        private void SubjectTasksStudent_Load(object sender, EventArgs e) {
            label1.Text = Subject;
            Services.fillDGV(dataGridView1, Services.getOrder(LectDir), "View");
            Services.fillDGV(dataGridView2, Services.getOrder(TestDir), "Start");
            if (string.Compare(Subject, "English") == 0 && string.Compare(StudentMainMenu.Student.Group, "121.2") == 0 && DateTime.Now.Month >= 9 && DateTime.Now.Month < 12)
                if (!(DateTime.Now.Month == 9 && DateTime.Now.Day < 21))
                    button2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            StudentMainMenu.Show();
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string lectName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(lectName)) {
                Lecture lect = Services.deserializeObj<Lecture>(LectDir + lectName + ".json");
                ViewLecture vl = new ViewLecture(this, lect);
                vl.Show();
                Hide();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string testName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(testName)) {
                Test test = Services.deserializeObj<Test>(TestDir + testName + ".json");
                if (test.StudentMarksList.Exists(x => x.StudentUsrName == StudentMainMenu.Student.Username)) {
                    MessageBox.Show("You have already passed this test");
                    return;
                }
                if (test.Questions.Count == 0) {
                    MessageBox.Show("Test isn't ready yet!");
                    return;
                }
                ViewTest vt = new ViewTest(this, test);
                vt.Show();
                Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            Lecture cp = Services.deserializeObj<Lecture>("cp/cp.json");
            ViewLecture vl = new ViewLecture(this, cp);
            vl.Show();
            Hide();
        }
    }
}