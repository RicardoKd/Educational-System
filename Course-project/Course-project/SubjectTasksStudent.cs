using System;
using System.Windows.Forms;

namespace Course_project {

    public partial class SubjectTasksStudent : Form {
        public string Subject { get; set; }
        public string LectDir { get; set; } // with semester
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
            Services.fillDGV(dataGridView1, Services.getOrder(LectDir), "View"); // Fill lecture list
            Services.fillDGV(dataGridView2, Services.getOrder(TestDir), "Start"); // Fill test list
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
                ViewTest vt = new ViewTest(this, test);
                vt.Show();
                Hide();
            }
        }
    }
}