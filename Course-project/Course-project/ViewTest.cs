using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class ViewTest : Form {
        public SubjectTasksStudent SubjectTasksStudent { get; set; }
        public Test Test { get; set; }
        public DateTime StartTime { get; set; }
        public int QuestionInd { get; set; }
        public TestMark CurrentMark { get; set; }
        public List<TestQuestion> NewOrder { get; set; }

        public ViewTest(SubjectTasksStudent subjectTasksStudent, Test test) {
            InitializeComponent();
            SubjectTasksStudent = subjectTasksStudent;
            Test = test;
            QuestionInd = 0;
            CurrentMark = new TestMark {
                Marks = new List<int>(),
                StudentUsrName = SubjectTasksStudent.StudentMainMenu.Student.Username
            };
            if (Test.RandQuestionOrder)
                NewOrder = Services.randomizeList(Test.Questions); // here we use local test variable, because lists are sent as parameters by reference
            else
                NewOrder = new List<TestQuestion>(Test.Questions);
        }

        private void ViewTest_Load(object sender, EventArgs e) {
            StartTime = DateTime.Now;
            NextQ();
        }

        private void button1_Click(object sender, EventArgs e) { // End test
            CurrentMark.TimeSpent = DateTime.Now - StartTime;
            saveResult();
            Test.StudentMarksList.Add(CurrentMark);
            DialogResult dr = MessageBox.Show("Are you sure you want to end test?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK) {
                SubjectTasksStudent.Show();
                Close();
            }
            //order marks in CurrentMark according to initial

            // Save result to file
        }

        private void button6_Click(object sender, EventArgs e) { // Next
            saveResult();
            QuestionInd++;
            NextQ();
        }

        private void NextQ() {
            label1.Text = NewOrder[QuestionInd].Question;
            Services.nexTQuestion(this, NewOrder, QuestionInd, richTextBox1);
            if (QuestionInd == Test.Questions.Count - 1) {
                button1.Visible = true;
                button6.Visible = false;
            }
        }

        private void saveResult() {
            //Save
            TestQuestion currentQ = NewOrder[QuestionInd];
            int finalMark = 0;
            if (currentQ.WrongAns.Count == 0) { // detailed answer
                int diff = string.Compare(richTextBox1.Text, currentQ.RightAns[0]);
                CurrentMark.Marks.Add(currentQ.Value - diff);
                finalMark = diff >= 0 ? currentQ.Value - diff : 0;
                richTextBox1.Visible = false;
            } else {
                if (currentQ.RightAns.Count == 1) { // only one right answer
                    List<RadioButton> allRadBtn = Services.getCollection<RadioButton>(this);

                    // Next 2 loops can be combined
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

                    // Next 2 loops can be combined
                    foreach (CheckBox cb in allChkBox) {
                        if (cb.Checked && currentQ.RightAns.Contains(cb.Text))
                            checkedRightAnsCount++;
                    }
                    foreach (CheckBox cb in allChkBox)
                        Controls.Remove(cb);

                    finalMark = (currentQ.Value / currentQ.RightAns.Count) * checkedRightAnsCount;
                }
            }
            CurrentMark.Marks.Add(finalMark);
        }
    }
}