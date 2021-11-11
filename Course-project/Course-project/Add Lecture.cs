using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class Add_Lecture : Form {
        private int currentImg;
        public bool Changed { get; set; }
        public bool EditMode { get; set; }
        public GroupInfo GrInfoForm { get; set; }
        public int CurrentImg { get => currentImg; set => currentImg = value; }
        public Lecture Lecture { get; set; }
        public string Dir { get; set; } // without semester

        // Constructor for creating a new lect
        public Add_Lecture(GroupInfo grInfoForm) {
            InitializeComponent();
            GrInfoForm = grInfoForm;
            EditMode = false;
            CurrentImg = -1;
            Lecture = new Lecture();
            comboBox3.SelectedIndex = 0;
        }

        // Constructor for editing a lect
        public Add_Lecture(GroupInfo grInfoForm, Lecture lecture) {
            InitializeComponent();
            Text = "Edit lecture";
            GrInfoForm = grInfoForm;
            Lecture = lecture;
            EditMode = true;
            if (lecture.ImgList.Count != 0) { // check if empty
                CurrentImg = 0;
                pictureBox1.Image = Image.FromFile(lecture.ImgList[CurrentImg]);
            } else {
                CurrentImg = -1;
                lecture.ImgList = new List<string>();
            }
            comboBox3.Text = Convert.ToString(lecture.Semester);
            textBox1.Text = lecture.Name;
            richTextBox1.Text = lecture.Text;
        }

        private void Add_Lecture_Load(object sender, EventArgs e) {
            Dir = "Lectures/" + GrInfoForm.CurGr.Specialty + "/" + GrInfoForm.CurGr.Year + "/" + GrInfoForm.TeacherMM.Teacher.Subject + "/";
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog2.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(GrInfoForm.CurGr.Name, GrInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Add img
            if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            string imgName = openFileDialog2.FileName;
            Lecture.ImgList.Add(imgName);
            pictureBox1.Image = Image.FromFile(imgName);
            if (CurrentImg == -1)
                CurrentImg += 1;
            else
                CurrentImg = Lecture.ImgList.Count - 1;
        }

        private void button3_Click(object sender, EventArgs e) { // Save
            Changed = false;
            string name = Convert.ToString(textBox1.Text);
            string newText = Convert.ToString(richTextBox1.Text);
            int newSemester = Convert.ToInt32(comboBox3.SelectedItem);
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(newText)) {
                MessageBox.Show("Fill in all cells!");
                return;
            }
            if (EditMode) {
                List<string> lectOrder = Services.getOrder(Dir + Lecture.Semester);
                int lectInd = lectOrder.FindIndex(x => x.Equals(Lecture.Name));

                File.Delete(Dir + Lecture.Semester + "/" + lectOrder[lectInd] + ".json"); // delete old version
                if (newSemester == Lecture.Semester) {
                    lectOrder[lectInd] = name;
                    Services.rewriteOrder(Dir + newSemester, lectOrder);
                } else {
                    lectOrder.RemoveAt(lectInd); // delete the lecture because it's moved to another semester
                    Services.rewriteOrder(Dir + Lecture.Semester, lectOrder);
                    Services.appendToOrder(Dir + newSemester, name);
                }
            } else
                Services.appendToOrder(Dir + newSemester, name);
            Lecture lect = new Lecture(name, newText, newSemester, Lecture.ImgList);
            lect.WriteToJson(Dir + newSemester + "/");
            MessageBox.Show("The lecture is succesfuly added!");
            GroupInfo grInfo = new GroupInfo(GrInfoForm.CurGr.Name, GrInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button4_Click(object sender, EventArgs e) { // Import text
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = File.ReadAllText(filename);
            richTextBox1.Text = fileText;
        }

        private void button5_Click(object sender, EventArgs e) {
            Services.previousImg(ref currentImg, Lecture, pictureBox1);
        }

        private void button6_Click(object sender, EventArgs e) {
            Services.nextImg(ref currentImg, Lecture, pictureBox1);
        }

        private void button7_Click(object sender, EventArgs e) { // Delete img
            if (CurrentImg == -1)
                return;
            if (Lecture.ImgList.Count == 1) {
                Lecture.ImgList.RemoveAt(CurrentImg);
                pictureBox1.Image = null;
                CurrentImg = -1;
                return;
            }
            if (CurrentImg == Lecture.ImgList.Count - 1) {
                Lecture.ImgList.RemoveAt(CurrentImg);
                pictureBox1.Image = Image.FromFile(Lecture.ImgList[0]);
                CurrentImg = 0;
                return;
            } else if (CurrentImg == 0) {
                Lecture.ImgList.RemoveAt(CurrentImg);

                pictureBox1.Image = Image.FromFile(Lecture.ImgList[Lecture.ImgList.Count - 1]);
                CurrentImg = Lecture.ImgList.Count - 1;
                return;
            } else {
                Lecture.ImgList.RemoveAt(CurrentImg);
                pictureBox1.Image = Image.FromFile(Lecture.ImgList[CurrentImg + 1]);
                CurrentImg = CurrentImg + 1;
                return;
            }
        }
    }
}