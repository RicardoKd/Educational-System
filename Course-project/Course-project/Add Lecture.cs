using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class Add_Lecture : Form {
        private Group curGr;
        private TeacherMainMenu teacherMM;
        private string dir; // without semester
        private bool editMode;

        private List<string> imgList = new List<string>(); // for images
        private int currentPic = -1; // for images

        private int semester; // needed when this form is used to edit lect
        private string[] lectOrder; // needed when this form is used to edit lect
        private int curLectInd; // needed when this form is used to edit lect

        // Constructor for creating a new lect
        public Add_Lecture(Group curGr, TeacherMainMenu teacherMM) {
            InitializeComponent();
            this.curGr = curGr;
            this.teacherMM = teacherMM;
            dir = "Lectures/" + curGr.Specialty + "/" + curGr.Year + "/" + teacherMM.Teacher.Subject + "/";
            editMode = false;
        }

        // Constructor for editing a lect
        public Add_Lecture(Group curGr, Lecture lect, TeacherMainMenu teacherMM) {
            InitializeComponent();
            Text = "Edit lecture";
            this.curGr = curGr;
            this.teacherMM = teacherMM;
            semester = lect.Semester;
            editMode = true;
            comboBox3.Text = Convert.ToString(semester);
            textBox1.Text = lect.Name;
            richTextBox1.Text = lect.Text;
            imgList.AddRange(lect.ImgList);
            dir = "Lectures/" + curGr.Specialty + "/" + curGr.Year + "/" + teacherMM.Teacher.Subject + "/";
            // To save the index of the current lect
            StreamReader r = new StreamReader(File.Open(@dir + semester + "/order.txt", FileMode.Open));
            lectOrder = r.ReadToEnd().Split(",", StringSplitOptions.RemoveEmptyEntries);
            r.Close();
            for (curLectInd = 0; curLectInd < lectOrder.Length; curLectInd++)
                if (string.Compare(lectOrder[curLectInd], lect.Name) == 0)
                    break;
        }

        private void Add_Lecture_Load(object sender, EventArgs e) {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog2.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(curGr.Name, teacherMM);
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
                File.Delete(@dir + semester + "/" + lectOrder[curLectInd] + ".json"); // delete old version
                if (newSemester == semester) {
                    lectOrder[curLectInd] = newName;
                    StreamWriter wr = new StreamWriter(File.Open(@dir + semester + "/order.txt", FileMode.Create));
                    foreach (string lectName in lectOrder)
                        wr.Write(lectName + ",");
                    wr.Close();
                } else {
                    lectOrder[curLectInd] = ""; // delete the lecture because it's moved to another semester
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