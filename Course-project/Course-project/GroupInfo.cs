using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class GroupInfo : Form {
        private Group CurGr;
        private TeacherMainMenu teacherMM;
        private int lectQuantity;
        private int semester = DateTime.Now.Month > 6 ? 1 : 2;
        private List<Lecture> lectList;

        public GroupInfo(string grName, TeacherMainMenu teacherMM) {
            InitializeComponent();
            CurGr = new Group(grName);
            this.teacherMM = teacherMM;
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
            string dir = "Lectures/" + CurGr.Specialty + "/" + CurGr.Year + "/" + semester + "/";
            List<string> lectFilesList = new List<string>(Directory.GetFiles(dir));
            lectQuantity = lectFilesList.Count;
            MessageBox.Show("lectQuantity: " + lectQuantity);
            i = 0;
            lectList = new List<Lecture>();
            if (lectQuantity > 0) {
                foreach (string path in lectFilesList) {
                    StreamReader r = new StreamReader(File.Open(path, FileMode.Open));
                    string json = r.ReadToEnd();
                    r.Close();
                    Lecture curLect = JsonConvert.DeserializeObject<Lecture>(json);
                    lectList.Add(curLect);
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = i + 1;
                    dataGridView2.Rows[i].Cells[1].Value = curLect.Name;
                    i++;
                }
            }

            // Fill test list
        }

        private void button1_Click_1(object sender, EventArgs e) { // Back
            teacherMM.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Add lecture
            Add_Lecture al = new Add_Lecture(CurGr, lectQuantity, teacherMM);
            al.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e) { // Save order (both for lectures and tests)
            // Save lecture order
            List<Lecture> newLectOrder = new List<Lecture>();
            for (int i = 0; i < dataGridView2.RowCount - 1; i++) {
                Lecture lecture = new Lecture();
                lecture.Name = (string)dataGridView2.Rows[i].Cells[1].Value;
                lecture.Index = i + 1; // save new index
                foreach (Lecture lc in lectList)
                    if (string.Compare(lc.Name, lecture.Name) == 0) {
                        lecture.Text = lc.Text;
                        lecture.ImgList = lc.ImgList;
                        break;
                    }
                string dir = "Lectures/" + CurGr.Specialty + "/" + CurGr.Year + "/" + semester + "/";
                lecture.WriteToJson(dir);
            }
            MessageBox.Show("New order succesfully saved");

            // Save test order
        }

        // For reordering rows of lectures
        private int rowIndexFromMouseDown;

        private DataGridViewRow rw;

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e) {
            if (dataGridView2.SelectedRows.Count == 1) {
                if (e.Button == MouseButtons.Left) {
                    rw = dataGridView2.SelectedRows[0];
                    rowIndexFromMouseDown = rw.Index;
                    dataGridView2.DoDragDrop(rw, DragDropEffects.Move);
                }
            }
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e) {
            if (dataGridView2.SelectedRows.Count > 0) {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e) {
            Point clientPoint = dataGridView2.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dataGridView2.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView2.Rows.Insert(dataGridView2.HitTest(clientPoint.X, clientPoint.Y).RowIndex, rw);
            }
        }
    }
}