using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class AddTest : Form {
        private GroupInfo grInfoForm;
        private Test test;
        private string dir; // without semester
        private bool changed;
        private bool editMode;
        private bool savedToOrder; // true when this from is loaded for the first

        private int semester; // needed when this form is used to edit test
        private string[] testOrder; // needed when this form is used to edit test
        private int curTestInd; // needed when this form is used to edit test

        // Constructor for creating new tests
        public AddTest(GroupInfo grInfoForm) {
            InitializeComponent();
            this.grInfoForm = grInfoForm;
            test = new Test();
            changed = false;
            editMode = false;
            savedToOrder = false;
            dir = "Tests/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            FormClosing += new FormClosingEventHandler(beforeClosing);
            comboBox3.SelectedIndex = 0;
        }

        // Constructor for editing tests
        public AddTest(GroupInfo grInfoForm, Test test, bool savedToOrder = false) {
            InitializeComponent();
            this.grInfoForm = grInfoForm;

            semester = test.Semester;
            dir = "Tests/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            changed = false;
            this.test = test;
            editMode = true;
            this.savedToOrder = savedToOrder;
            textBox1.Text = test.Name;
            comboBox3.Text = Convert.ToString(semester);
            checkBox1.Checked = test.RandQuestionOrder == true ? true : false;
            if (savedToOrder) {
                Text = "Edit test";
                StreamReader r = new StreamReader(File.Open(@dir + semester + "/order.txt", FileMode.Open));
                testOrder = r.ReadToEnd().Split(",", StringSplitOptions.RemoveEmptyEntries);
                r.Close();
                for (curTestInd = 0; curTestInd < testOrder.Length; curTestInd++)
                    if (string.Compare(testOrder[curTestInd], test.Name) == 0)
                        break;
                MessageBox.Show("curTestInd = " + curTestInd);
            }
            int i = 0;
            foreach (TestQuestion q in test.Questions) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = q.Question;
                dataGridView1.Rows[i].Cells[2].Value = "Edit";
                i++;
            }
            // To save the index of the current test

            FormClosing += new FormClosingEventHandler(beforeClosing);
        }

        public GroupInfo GrInfoForm { get => grInfoForm; set => grInfoForm = value; }
        public Test Test { get => test; set => test = value; }
        public bool SavedToOrder { get => savedToOrder; set => savedToOrder = value; }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(grInfoForm.CurGr.Name, grInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Save
            changed = false;
            string newTName = textBox1.Text;
            bool randQOrder = checkBox1.Checked == true ? true : false;
            int newSemester = Convert.ToInt32(comboBox3.SelectedItem);
            if (string.IsNullOrEmpty(newTName)) {
                MessageBox.Show("Fill in test name field!");
                return;
            }

            if (editMode && savedToOrder) {
                File.Delete(@dir + semester + "/" + testOrder[curTestInd] + ".json"); // delete old version
                if (newSemester == semester) {
                    testOrder[curTestInd] = newTName;
                    StreamWriter wr = new StreamWriter(File.Open(@dir + semester + "/order.txt", FileMode.Create));
                    foreach (string lectName in testOrder)
                        wr.Write(lectName + ",");
                    wr.Close();
                } else {
                    testOrder[curTestInd] = ""; // delete the lecture because it's moved to another semester
                    /*
                      Error happens here:
                    directory of new semester not found
                     */
                    StreamWriter wr = new StreamWriter(File.Open(@dir + newSemester + "/order.txt", FileMode.Append));
                    wr.Write(newTName + ",");
                    wr.Close();
                }
            } else {
                if (!Directory.Exists(@dir + newSemester))
                    Directory.CreateDirectory(@dir + newSemester);
                StreamWriter wr = new StreamWriter(File.Open(@dir + newSemester + "/order.txt", FileMode.Append));
                wr.Write(newTName + ",");
                wr.Close();
                savedToOrder = true;
            }
            Test newTest = new Test(newTName, test.Questions, randQOrder, newSemester);
            newTest.WriteToJson(@dir + newSemester + "/");
            MessageBox.Show("The test is succesfuly saved!");
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
                TestQuestion clickedQ = new TestQuestion();
                foreach (TestQuestion q in test.Questions)
                    if (string.Compare(clickedQText, q.Question) == 0)
                        clickedQ = q;

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
            test.Name = textBox1.Text;
            changed = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {
            test.Semester = Convert.ToInt32(comboBox3.SelectedItem);
            changed = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            changed = true;
            test.RandQuestionOrder = checkBox1.Checked == true ? true : false;
            if (checkBox1.Checked)
                button4.Enabled = false;
            else
                button4.Enabled = true;
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