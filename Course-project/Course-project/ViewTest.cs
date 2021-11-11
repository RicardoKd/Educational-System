using System;
using System.Windows.Forms;

namespace Course_project {

    public partial class ViewTest : Form {
        public SubjectTasksStudent SubjectTasksStudent { get; set; }
        public Test Test { get; set; }

        public ViewTest(SubjectTasksStudent subjectTasksStudent, Test test) {
            InitializeComponent();
            SubjectTasksStudent = subjectTasksStudent;
            Test = test;
        }

        private void ViewTest_Load(object sender, EventArgs e) {
        }
    }
}