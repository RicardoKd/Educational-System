using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class AddTest : Form {
        public bool Changed { get; set; }
        public bool EditMode { get; set; }
        public bool SavedToOrder { get; set; } // true when this from is loaded for the first
        public GroupInfo GrInfoForm { get; set; }
        public string Dir { get; set; } // without semester
        public string OldName { get; set; }
        public Test Test { get; set; }

        // Constructor for creating new tests
        public AddTest(GroupInfo grInfoForm) {
            InitializeComponent();
            GrInfoForm = grInfoForm;
            Test = new Test();
            Changed = false;
            EditMode = false;
            SavedToOrder = false;
            Dir = "Tests/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            FormClosing += new FormClosingEventHandler(beforeClosing);
            comboBox3.SelectedIndex = 0;
        }

        // Constructor for editing tests
        public AddTest(GroupInfo grInfoForm, Test test, bool savedToOrder = false) {
            InitializeComponent();
            GrInfoForm = grInfoForm;
            Test = test;
            SavedToOrder = savedToOrder;
            Changed = false;
            EditMode = true;
            OldName = test.Name;
            Dir = "Tests/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            textBox1.Text = test.Name;
            comboBox3.Text = Convert.ToString(test.Semester);
            checkBox1.Checked = test.RandQuestionOrder == true ? true : false;
            if (savedToOrder)
                Text = "Edit test";
            Services.fillDGV(dataGridView1, test.getQuestions(), "View");
            FormClosing += new FormClosingEventHandler(beforeClosing);
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(GrInfoForm.CurGr.Name, GrInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Save
            Changed = false;
            string newTName = textBox1.Text;
            bool randQOrder = checkBox1.Checked == true ? true : false;
            int newSemester = Convert.ToInt32(comboBox3.SelectedItem);
            if (string.IsNullOrEmpty(newTName)) {
                MessageBox.Show("Fill in test name field!");
                return;
            }

            if (EditMode && SavedToOrder) {
                List<string> testOrder = Services.getOrder(Dir + Test.Semester);
                int testInd = testOrder.FindIndex(x => x.Equals(OldName));

                File.Delete(Dir + Test.Semester + "/" + testOrder[testInd] + ".json"); // delete old version
                if (newSemester == Test.Semester) {
                    testOrder[testInd] = newTName;
                    StreamWriter wr = new StreamWriter(File.Open(Dir + Test.Semester + "/order.txt", FileMode.Create));
                    foreach (string lectName in testOrder)
                        wr.Write(lectName + ",");
                    wr.Close();
                } else {
                    testOrder.RemoveAt(testInd); // delete the lecture because it's moved to another semester
                    /*
                      Error happens here:
                    directory of new semester not found
                     */
                    StreamWriter wr = new StreamWriter(File.Open(Dir + newSemester + "/order.txt", FileMode.Append));
                    wr.Write(newTName + ",");
                    wr.Close();
                }
            } else {
                if (!Directory.Exists(Dir + newSemester))
                    Directory.CreateDirectory(Dir + newSemester);
                StreamWriter wr = new StreamWriter(File.Open(Dir + newSemester + "/order.txt", FileMode.Append));
                wr.Write(newTName + ",");
                wr.Close();
                SavedToOrder = true;
            }
            Test newTest = new Test(newTName, Test.Questions, randQOrder, newSemester);
            newTest.WriteToJson(Dir + newSemester + "/");
            MessageBox.Show("The test is succesfuly saved!");
            GroupInfo grInfo = new GroupInfo(GrInfoForm.CurGr.Name, GrInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e) { // Add question
            Add_Test_Question atq = new Add_Test_Question(this);
            atq.Show();
            Hide();
        }

        private void button4_Click(object sender, EventArgs e) { // Save order
            if (dataGridView1.RowCount - 1 > 0) { // check if empty
                for (int i = 0; i < dataGridView1.RowCount - 1; i++) { }
                // chanche the order of questions in test.Questions
            }
        }

        // "Edit" btn OnClick in question list
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) {
                string clickedQText = (string)senderGrid.Rows[e.RowIndex].Cells[1].Value;
                TestQuestion clickedQ = Test.Questions.Find(x => x.Question.Contains(clickedQText));
                Add_Test_Question tq = new Add_Test_Question(this, clickedQ);
                tq.Show();
                Hide();
            }
        }

        private void beforeClosing(object sender, FormClosingEventArgs e) {
            /*if (changed) {
                DialogResult dr = MessageBox.Show("Data is not saved. Want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) {
                    e.Cancel = true;
                    return;
                }
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            /*test.Name = textBox1.Text;*/
            Changed = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {
            /*test.Semester = Convert.ToInt32(comboBox3.SelectedItem);*/
            Changed = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            Changed = true;
            /*test.RandQuestionOrder = checkBox1.Checked == true ? true : false;
            if (checkBox1.Checked)
                button4.Enabled = false;
            else
                button4.Enabled = true;*/
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
        }

        // For reordering questions
        private int rowIndexFromMouseDown;

        private DataGridViewRow rw;

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e) {
            if (dataGridView1.SelectedRows.Count == 1)
                if (e.Button == MouseButtons.Left) {
                    rw = dataGridView1.SelectedRows[0];
                    rowIndexFromMouseDown = rw.Index;
                    dataGridView1.DoDragDrop(rw, DragDropEffects.Move);
                }
        }

        private void dataGridView1_DragEnter(object sender, DragEventArgs e) {
            if (dataGridView1.SelectedRows.Count > 0)
                e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e) {
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView1.Rows.Insert(dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex, rw);
            }
        }
    }
}