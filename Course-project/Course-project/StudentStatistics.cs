using System;
using System.Windows.Forms;

namespace Course_project {

    public partial class StudentStatistics : Form {
        public GroupInfo GroupInfoForm { get; set; }
        public Student Student { get; set; }

        public StudentStatistics(GroupInfo groupInfoForm, Student student) {
            InitializeComponent();
            GroupInfoForm = groupInfoForm;
            Student = student;
        }

        private void button1_Click(object sender, EventArgs e) {
            GroupInfoForm.Show();
            Close();
        }
    }
}