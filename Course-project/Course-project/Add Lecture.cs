using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class Add_Lecture : Form {
        private GroupInfo grInfoForm;
        private string dir; // without semester
        private bool editMode;
        private Lecture lecture;
        private List<string> imgList = new List<string>(); // for images
        private int currentPic = -1; // for images

        // Constructor for creating a new lect
        public Add_Lecture(GroupInfo grInfoForm) {
            InitializeComponent();
            this.grInfoForm = grInfoForm;
            dir = "Lectures/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            editMode = false;
        }

        // Constructor for editing a lect
        public Add_Lecture(GroupInfo grInfoForm, Lecture lecture) {
            InitializeComponent();
            Text = "Edit lecture";
            this.grInfoForm = grInfoForm;
            this.lecture = lecture;
            editMode = true;
            dir = "Lectures/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            comboBox3.Text = Convert.ToString(lecture.Semester);
            textBox1.Text = lecture.Name;
            richTextBox1.Text = lecture.Text;
            imgList.AddRange(lecture.ImgList);
        }

        private void Add_Lecture_Load(object sender, EventArgs e) {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog2.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(grInfoForm.CurGr.Name, grInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Add img
            if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            string imgName = openFileDialog2.FileName;
            imgList.Add(imgName);
            pictureBox1.Image = Image.FromFile(imgName);
            if (currentPic == -1)
                currentPic += 1;
            else
                currentPic = imgList.Count - 1;
            MessageBox.Show("Current picture: " + currentPic);
        }

        private void button3_Click(object sender, EventArgs e) { // Save
            string newName = Convert.ToString(textBox1.Text);
            string newText = Convert.ToString(richTextBox1.Text);
            int newSemester = Convert.ToInt32(comboBox3.SelectedItem) == 1 ? 1 : 2;
            if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newText)) {
                MessageBox.Show("Fill in all cells!");
                return;
            }

            if (editMode) {
                List<string> lectOrder = Services.getOrder(dir + lecture.Semester);
                int lectInd = lectOrder.FindIndex(x => x.Equals(lecture.Name));
                MessageBox.Show("curTestInd = " + lectInd);

                File.Delete(@dir + lecture.Semester + "/" + lectOrder[lectInd] + ".json"); // delete old version
                if (newSemester == lecture.Semester) {
                    lectOrder[lectInd] = newName;
                    StreamWriter wr = new StreamWriter(File.Open(@dir + lecture.Semester + "/order.txt", FileMode.Create));
                    foreach (string lectName in lectOrder)
                        wr.Write(lectName + ",");
                    wr.Close();
                } else {
                    lectOrder[lectInd] = ""; // delete the lecture because it's moved to another semester
                    /*
                      Error happens here:
                    directory of new semester not found
                     */
                    StreamWriter wr = new StreamWriter(File.Open(@dir + newSemester + "/order.txt", FileMode.Append));
                    wr.Write(newName + ",");
                    wr.Close();
                }
            } else {
                if (!Directory.Exists(@dir + newSemester))
                    Directory.CreateDirectory(@dir + newSemester);
                StreamWriter wr = new StreamWriter(File.Open(@dir + newSemester + "/order.txt", FileMode.Append));
                wr.Write(newName + ",");
                wr.Close();
            }
            Lecture lect = new Lecture(newName, newText, newSemester, imgList);
            lect.WriteToJson(@dir + newSemester + "/");
            MessageBox.Show("The lecture is succesfuly added!");
            GroupInfo grInfo = new GroupInfo(grInfoForm.CurGr.Name, grInfoForm.TeacherMM);
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
    }
}