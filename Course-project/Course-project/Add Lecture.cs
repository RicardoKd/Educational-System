using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Course_project {

    public partial class Add_Lecture : Form {
        private Group CurGr;
        private TeacherMainMenu teacherMM;
        private int lectQuantity;

        private List<string> imgList = new List<string>();
        private int currentPic = -1;

        public Add_Lecture(Group CurGr, int lectQuantity, TeacherMainMenu teacherMM) {
            InitializeComponent();
            this.CurGr = CurGr;
            this.teacherMM = teacherMM;
            this.lectQuantity = lectQuantity;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog2.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*";
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Add_Lecture_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(CurGr.Name, teacherMM);
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
            string lectName = Convert.ToString(textBox1.Text);
            string text = Convert.ToString(richTextBox1.Text);
            int semester = Convert.ToInt32(comboBox3.SelectedItem);

            if (string.IsNullOrEmpty(lectName) || string.IsNullOrEmpty(text)) {
                MessageBox.Show("Fill in all cells.");
                return;
            }

            string dir = "Lectures/" + CurGr.Specialty + "/" + CurGr.Year + "/" + semester + "/";

            Lecture lect = new Lecture(lectName, text, lectQuantity + 1, imgList);
            lect.WriteToJson(dir);

            MessageBox.Show("The lecture is succesfuly added!");
            GroupInfo grInfo = new GroupInfo(CurGr.Name, teacherMM);
            grInfo.Show();
            Close();
        }

        private void button4_Click(object sender, EventArgs e) { // Import text
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = fileText;
        }
    }
}