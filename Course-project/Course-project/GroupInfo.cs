using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class GroupInfo : Form {
        private Group curGr;
        private TeacherMainMenu teacherMM;
        private string lectDir; // with semester
        private string testDir; // with semester

        private int rowIndexFromMouseDown; // For reordering rows
        private DataGridViewRow rw; // For reordering rows

        public GroupInfo(string grName, TeacherMainMenu teacherMM) {
            InitializeComponent();
            curGr = new Group(grName);
            this.teacherMM = teacherMM;
            int semester = DateTime.Now.Month > 6 ? 1 : 2;
            lectDir = "Lectures/" + curGr.Specialty + "/" + curGr.Year + "/" + teacherMM.Teacher.Subject + "/" + semester + "/";
            testDir = "Tests/" + curGr.Specialty + "/" + curGr.Year + "/" + teacherMM.Teacher.Subject + "/" + semester + "/";
        }

        public TeacherMainMenu TeacherMM { get => teacherMM; set => teacherMM = value; }
        public Group CurGr { get => curGr; set => curGr = value; }

        private void GroupInfo_Load(object sender, EventArgs e) {
            label1.Text = curGr.Name;
            // Fill student list
            int i = 0;
            foreach (string stName in curGr.Students) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = stName;
                i++;
            }
            // Fill lecture list
            if (Directory.Exists(lectDir)) {
                StreamReader r = new StreamReader(File.Open(lectDir + "order.txt", FileMode.OpenOrCreate));
                List<string> lectFilesList = new List<string>(r.ReadToEnd().Split(",", StringSplitOptions.RemoveEmptyEntries));
                r.Close();
                i = 0;
                foreach (string lectName in lectFilesList) {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = i + 1;
                    dataGridView2.Rows[i].Cells[1].Value = lectName;
                    dataGridView2.Rows[i].Cells[2].Value = "Edit";
                    i++;
                }
            }
            // Fill test list
            if (Directory.Exists(testDir)) {
                StreamReader r = new StreamReader(File.Open(testDir + "order.txt", FileMode.OpenOrCreate));
                List<string> testFilesList = new List<string>(r.ReadToEnd().Split(",", StringSplitOptions.RemoveEmptyEntries));
                r.Close();
                i = 0;
                foreach (string testName in testFilesList) {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[i].Cells[0].Value = i + 1;
                    dataGridView3.Rows[i].Cells[1].Value = testName;
                    dataGridView3.Rows[i].Cells[2].Value = "Edit";
                    i++;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e) { // Back
            teacherMM.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Add lecture
            Add_Lecture al = new Add_Lecture(this);
            al.Show();
            Close();
        }

        private void button4_Click(object sender, EventArgs e) { // Add test
            AddTest at = new AddTest(this);
            at.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e) { // Save order (both for lectures and tests)
            // Save lecture order
            if (dataGridView2.RowCount - 1 > 0) { // check if empty
                StreamWriter wr = new StreamWriter(File.Open(lectDir + "order.txt", FileMode.Create));
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    wr.Write((string)dataGridView2.Rows[i].Cells[1].Value + ",");
                wr.Close();
            }
            if (dataGridView3.RowCount - 1 > 0) {
                // Save test order
                StreamWriter wr = new StreamWriter(File.Open(testDir + "order.txt", FileMode.Create));
                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                    wr.Write((string)dataGridView3.Rows[i].Cells[1].Value + ",");
                wr.Close();
                MessageBox.Show("New order succesfully saved!");
            }
        }

        // "Edit" btn OnClick in lecture list
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) {
                string lectName = (string)senderGrid.Rows[e.RowIndex].Cells[1].Value;
                StreamReader r = new StreamReader(File.Open(lectDir + lectName + ".json", FileMode.Open));
                Lecture lect = JsonConvert.DeserializeObject<Lecture>(r.ReadToEnd());
                r.Close();
                Add_Lecture al = new Add_Lecture(this, lect);
                al.Show();
                Hide();
            }
        }

        // "Edit" btn OnClick in test list
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) {
                string testName = (string)senderGrid.Rows[e.RowIndex].Cells[1].Value;
                StreamReader r = new StreamReader(File.Open(@testDir + testName + ".json", FileMode.Open));
                Test test = JsonConvert.DeserializeObject<Test>(r.ReadToEnd());
                r.Close();
                AddTest at = new AddTest(this, test, true);
                at.Show();
                Hide();
            }
        }

        // For reordering rows of lectures
        private void dataGridView2_MouseClick(object sender, MouseEventArgs e) {
            if (dataGridView2.SelectedRows.Count == 1)
                if (e.Button == MouseButtons.Left) {
                    rw = dataGridView2.SelectedRows[0];
                    rowIndexFromMouseDown = rw.Index;
                    dataGridView2.DoDragDrop(rw, DragDropEffects.Move);
                }
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e) {
            if (dataGridView2.SelectedRows.Count > 0)
                e.Effect = DragDropEffects.Move;
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e) {
            Point clientPoint = dataGridView2.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dataGridView2.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView2.Rows.Insert(dataGridView2.HitTest(clientPoint.X, clientPoint.Y).RowIndex, rw);
            }
        }

        // For reordering rows of tests
        private void dataGridView3_MouseClick(object sender, MouseEventArgs e) {
            if (dataGridView3.SelectedRows.Count == 1)
                if (e.Button == MouseButtons.Left) {
                    rw = dataGridView3.SelectedRows[0];
                    rowIndexFromMouseDown = rw.Index;
                    dataGridView3.DoDragDrop(rw, DragDropEffects.Move);
                }
        }

        private void dataGridView3_DragEnter(object sender, DragEventArgs e) {
            if (dataGridView3.SelectedRows.Count > 0)
                e.Effect = DragDropEffects.Move;
        }

        private void dataGridView3_DragDrop(object sender, DragEventArgs e) {
            Point clientPoint = dataGridView3.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dataGridView3.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView3.Rows.Insert(dataGridView3.HitTest(clientPoint.X, clientPoint.Y).RowIndex, rw);
            }
        }
    }
}