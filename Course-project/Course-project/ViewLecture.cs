using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class ViewLecture : Form {
        public SubjectTasksStudent SubjectTasksStudent { get; set; }
        public Lecture Lecture { get; set; }

        public ViewLecture(SubjectTasksStudent subjectTasksStudent, Lecture lecture) {
            InitializeComponent();
            SubjectTasksStudent = subjectTasksStudent;
            Lecture = lecture;
        }

        private void ViewLecture_Load(object sender, EventArgs e) {
            label1.Text = Lecture.Name;
            richTextBox1.Text = Lecture.Text;
        }

        private void button1_Click(object sender, EventArgs e) {
            SubjectTasksStudent.Show();
            Close();
        }
    }
}