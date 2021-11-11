using System;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class GroupInfo : Form {
        public Group CurGr { get; set; }
        public string LectDir { get; set; } // with semester
        public string TestDir { get; set; } // with semester
        public TeacherMainMenu TeacherMM { get; set; }
        private int rowIndexFromMouseDown; // For reordering rows
        private DataGridViewRow rw;

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
                Lecture lect = Services.deserializeObj<Lecture>(LectDir + lectName + ".json");
                Add_Lecture al = new Add_Lecture(this, lect);
                al.Show();
                Close();
            }
        }

        // "Edit" btn OnClick in test list
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string testName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(testName)) {
                Test test = Services.deserializeObj<Test>(TestDir + testName + ".json");
                AddTest at = new AddTest(this, test, true);
                at.Show();
                Close();
            }
        }

        // For reordering rows of lectures
        private void dataGridView2_MouseClick(object sender, MouseEventArgs e) {
            Services.DGVMouseClick((DataGridView)sender, e, ref rw, ref rowIndexFromMouseDown);
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e) {
            Services.DGVDragEnter((DataGridView)sender, e);
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e) {
            Services.DGVragDrop((DataGridView)sender, e, rw, rowIndexFromMouseDown);
        }

        // For reordering rows of tests
        private void dataGridView3_MouseClick(object sender, MouseEventArgs e) {
            Services.DGVMouseClick((DataGridView)sender, e, ref rw, ref rowIndexFromMouseDown);
        }

        private void dataGridView3_DragEnter(object sender, DragEventArgs e) {
            Services.DGVDragEnter((DataGridView)sender, e);
        }

        private void dataGridView3_DragDrop(object sender, DragEventArgs e) {
            Services.DGVragDrop((DataGridView)sender, e, rw, rowIndexFromMouseDown);
        }
    }
}