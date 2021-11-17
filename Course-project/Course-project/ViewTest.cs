using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class ViewTest : Form {
        public SubjectTasksStudent SubjectTasksStudent { get; set; }
        public Test Test { get; set; }
        public DateTime StartTime { get; set; }
        public int QuestionInd { get; set; }
        public List<int> QuestionMarks { get; set; }
        public List<TestQuestion> NewOrder { get; set; }

        public ViewTest(SubjectTasksStudent subjectTasksStudent, Test test) {
            InitializeComponent();
            SubjectTasksStudent = subjectTasksStudent;
            Test = test;
            QuestionInd = 0;
            QuestionMarks = new List<int>();
            Test.StudentMarksList = new List<TestMark>();
            if (Test.RandQuestionOrder)
                NewOrder = Services.randomizeList(Test.Questions);
            else
                NewOrder = new List<TestQuestion>(Test.Questions);
        }

        private void ViewTest_Load(object sender, EventArgs e) {
            StartTime = DateTime.Now;
            NextQ();
        }

        private void button1_Click(object sender, EventArgs e) { // End test
            TimeSpan TimeSpent = DateTime.Now - StartTime;
            saveResult();
            DialogResult dr = MessageBox.Show("Are you sure you want to end test?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK) {
                TestMark tm;
                if (Test.RandQuestionOrder)
                    tm = Services.derandomizeMarks(this, TimeSpent);
                else
                    tm = new TestMark(SubjectTasksStudent.StudentMainMenu.Student.Username, QuestionMarks, TimeSpent);
                Test.StudentMarksList.Add(tm);
                Test.WriteToJson(SubjectTasksStudent.TestDir);
                SubjectTasksStudent.Show();
                Close();
            }
        }

        private void button6_Click(object sender, EventArgs e) { // Next
            saveResult();
            QuestionInd++;
            NextQ();
        }

        private void NextQ() {
            label1.Text = NewOrder[QuestionInd].Question;
            Services.nextQuestion(this, richTextBox1);
            if (QuestionInd == Test.Questions.Count - 1) {
                button1.Visible = true;
                button6.Visible = false;
            }
        }

        private void saveResult() {
            TestQuestion currentQ = NewOrder[QuestionInd];
            int finalMark = 0;
            if (currentQ.WrongAns.Count == 0) { // detailed answer
                int diff = string.Compare(richTextBox1.Text, currentQ.RightAns[0]);
                /*QuestionMarks.Add(currentQ.Value - diff);*/
                finalMark = diff >= 0 ? currentQ.Value - diff : 0;
                richTextBox1.Visible = false;
            } else {
                if (currentQ.RightAns.Count == 1) { // only one right answer
                    List<RadioButton> allRadBtn = Services.getCollection<RadioButton>(this);
                    foreach (RadioButton rb in allRadBtn)
                        if (rb.Checked && currentQ.RightAns.Contains(rb.Text)) {
                            finalMark = currentQ.Value;
                            break;
                        }
                    foreach (RadioButton rb in allRadBtn)
                        Controls.Remove(rb);
                } else if (currentQ.RightAns.Count > 1) { // multiple right answers
                    List<CheckBox> allChkBox = Services.getCollection<CheckBox>(this);
                    int checkedRightAnsCount = 0;
                    foreach (CheckBox cb in allChkBox)
                        if (cb.Checked && currentQ.RightAns.Contains(cb.Text))
                            checkedRightAnsCount++;
                    foreach (CheckBox cb in allChkBox)
                        Controls.Remove(cb);
                    finalMark = (currentQ.Value / currentQ.RightAns.Count) * checkedRightAnsCount;
                }
            }
            QuestionMarks.Add(finalMark);
        }
    }
}