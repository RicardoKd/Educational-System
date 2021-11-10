using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class GroupInfo : Form {
        public Group CurGr { get; set; }
        public string LectDir { get; set; } // with semester
        public string TestDir { get; set; } // with semester
        public TeacherMainMenu TeacherMM { get; set; }
        public int RowIndexFromMouseDown { get; set; } // For reordering rows
        public DataGridViewRow Rw { get; set; } // For reordering rows

        public GroupInfo(string grName, TeacherMainMenu teacherMM) {
            InitializeComponent();
            CurGr = new Group(grName);
            TeacherMM = teacherMM;
            int semester = Services.GetCurrentSemester();
            LectDir = "Lectures/" + CurGr.Specialty + "/" + CurGr.Year + "/" + teacherMM.Teacher.Subject + "/" + semester + "/";
            TestDir = "Tests/" + CurGr.Specialty + "/" + CurGr.Year + "/" + teacherMM.Teacher.Subject + "/" + semester + "/";
        }

        private void GroupInfo_Load(object sender, EventArgs e) {
            label1.Text = CurGr.Name;
            // Fill student list
            int i = 0;
            foreach (string stName in CurGr.Students) {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = stName;
                i++;
            }
            // Fill lecture list
            Services.fillDGV(dataGridView2, Services.getOrder(LectDir), "Edit");
            // Fill test list
            Services.fillDGV(dataGridView3, Services.getOrder(TestDir), "Edit");
        }

        private void button1_Click_1(object sender, EventArgs e) { // Back
            TeacherMM.Show();
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
                StreamWriter wr = new StreamWriter(File.Open(LectDir + "order.txt", FileMode.Create));
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    wr.Write((string)dataGridView2.Rows[i].Cells[1].Value + ",");
                wr.Close();
            }
            if (dataGridView3.RowCount - 1 > 0) {
                // Save test order
                StreamWriter wr = new StreamWriter(File.Open(TestDir + "order.txt", FileMode.Create));
                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                    wr.Write((string)dataGridView3.Rows[i].Cells[1].Value + ",");
                wr.Close();
                MessageBox.Show("New order succesfully saved!");
            }
        }

        // "Edit" btn OnClick in lecture list
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string lectName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(lectName)) {
                StreamReader r = new StreamReader(File.Open(LectDir + lectName + ".json", FileMode.Open));
                Lecture lect = JsonConvert.DeserializeObject<Lecture>(r.ReadToEnd());
                r.Close();
                Add_Lecture al = new Add_Lecture(this, lect);
                al.Show();
                Hide();
            }
        }

        // "Edit" btn OnClick in test list
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string testName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(testName)) {
                StreamReader r = new StreamReader(File.Open(TestDir + testName + ".json", FileMode.Open));
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
                    Rw = dataGridView2.SelectedRows[0];
                    RowIndexFromMouseDown = Rw.Index;
                    dataGridView2.DoDragDrop(Rw, DragDropEffects.Move);
                }
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e) {
            if (dataGridView2.SelectedRows.Count > 0)
                e.Effect = DragDropEffects.Move;
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e) {
            Point clientPoint = dataGridView2.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dataGridView2.Rows.RemoveAt(RowIndexFromMouseDown);
                dataGridView2.Rows.Insert(dataGridView2.HitTest(clientPoint.X, clientPoint.Y).RowIndex, Rw);
            }
        }

        // For reordering rows of tests
        private void dataGridView3_MouseClick(object sender, MouseEventArgs e) {
            if (dataGridView3.SelectedRows.Count == 1)
                if (e.Button == MouseButtons.Left) {
                    Rw = dataGridView3.SelectedRows[0];
                    RowIndexFromMouseDown = Rw.Index;
                    dataGridView3.DoDragDrop(Rw, DragDropEffects.Move);
                }
        }

        private void dataGridView3_DragEnter(object sender, DragEventArgs e) {
            if (dataGridView3.SelectedRows.Count > 0)
                e.Effect = DragDropEffects.Move;
        }

        private void dataGridView3_DragDrop(object sender, DragEventArgs e) {
            Point clientPoint = dataGridView3.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dataGridView3.Rows.RemoveAt(RowIndexFromMouseDown);
                dataGridView3.Rows.Insert(dataGridView3.HitTest(clientPoint.X, clientPoint.Y).RowIndex, Rw);
            }
        }
    }
}