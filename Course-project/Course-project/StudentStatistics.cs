using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class StudentStatistics : Form {
        public GroupInfo GroupInfoForm { get; set; }
        public string StudentUsrName { get; set; }
        public TimeSpan TotalTime { get; set; }

        public StudentStatistics(GroupInfo groupInfoForm, string studentUsrName) {
            InitializeComponent();
            GroupInfoForm = groupInfoForm;
            StudentUsrName = studentUsrName;
            TotalTime = TimeSpan.Zero;
        }

        private void StudentStatistics_Load(object sender, EventArgs e) {
            List<string> lectOrder = Services.getOrder(GroupInfoForm.LectDir);
            List<string> testOrder = Services.getOrder(GroupInfoForm.TestDir);
            Services.fillDGV(dataGridView1, lectOrder, "");
            Services.fillDGV(dataGridView2, testOrder, "");
            int i = 0;
            foreach (string lectName in lectOrder) {
                Lecture l = Services.deserializeObj<Lecture>(GroupInfoForm.LectDir + lectName + ".json");
                int studentIndex = l.StudentMarksList.FindIndex(x => x.StudentUsrName.Equals(StudentUsrName));
                if (studentIndex != -1) {
                    TimeSpan ts = l.StudentMarksList[studentIndex].TimeSpent;
                    dataGridView1.Rows[i].Cells[2].Value = ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
                    TotalTime += l.StudentMarksList[studentIndex].TimeSpent;
                } else
                    dataGridView1.Rows[i].Cells[2].Value = "Not read";
                i++;
            }
            i = 0;
            foreach (string testName in testOrder) {
                Test t = Services.deserializeObj<Test>(GroupInfoForm.TestDir + testName + ".json");
                int studentIndex = t.StudentMarksList.FindIndex(x => x.StudentUsrName.Equals(StudentUsrName));
                if (studentIndex != -1) {
                    TimeSpan ts = t.StudentMarksList[studentIndex].TimeSpent;
                    dataGridView2.Rows[i].Cells[2].Value = ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
                    dataGridView2.Rows[i].Cells[3].Value = t.getStudentMark(StudentUsrName);
                    TotalTime += t.StudentMarksList[studentIndex].TimeSpent;
                } else {
                    dataGridView2.Rows[i].Cells[2].Value = "Not passed";
                    dataGridView2.Rows[i].Cells[3].Value = "Not passed";
                }
                i++;
            }
            label1.Text += TotalTime.Hours + ":" + TotalTime.Minutes + ":" + TotalTime.Seconds;
        }

        private void button1_Click(object sender, EventArgs e) {
            GroupInfoForm.Show();
            Close();
        }
    }
}