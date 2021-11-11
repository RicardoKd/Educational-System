using System;
using System.Drawing;
using System.Windows.Forms;

namespace Course_project {

    public partial class ViewLecture : Form {
        private int currentImg;
        public int CurrentImg { get => currentImg; set => currentImg = value; }
        public Lecture Lecture { get; set; }
        public SubjectTasksStudent SubjectTasksStudent { get; set; }

        public ViewLecture(SubjectTasksStudent subjectTasksStudent, Lecture lecture) {
            InitializeComponent();
            SubjectTasksStudent = subjectTasksStudent;
            Lecture = lecture;
        }

        private void ViewLecture_Load(object sender, EventArgs e) {
            label1.Text = Lecture.Name;
            richTextBox1.Text = Lecture.Text;

            if (Lecture.ImgList.Count != 0) {
                CurrentImg = 0;
                pictureBox1.Image = Image.FromFile(Lecture.ImgList[CurrentImg]);
            } else
                CurrentImg = -1;
        }

        private void button1_Click(object sender, EventArgs e) {
            SubjectTasksStudent.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            Services.previousImg(ref currentImg, Lecture, pictureBox1);
        }

        private void button3_Click(object sender, EventArgs e) {
            Services.nextImg(ref currentImg, Lecture, pictureBox1);
        }
    }
}