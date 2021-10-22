using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class TeacherMainMenu : Form {
        private User user;

        public TeacherMainMenu(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void TeacherMainMenu_Load(object sender, EventArgs e) {
            label1.Text = (user as Teacher).Subject;
            label2.Text = "Hello, " + (user as Teacher).Username;

            StreamReader grListReader = new StreamReader(File.Open(@"groupList.txt", FileMode.OpenOrCreate));
            List<string> grList = new List<string>(grListReader.ReadToEnd().Split(" "));
            grListReader.Close();

            List<string> grListSort = new List<string>();
            foreach (string grName in grList) {
                string[] grNameArr = grName.Split(".");
                List<string> grSubjList = new List<string>(new Rules().getSubjList(grNameArr[0], grNameArr[1]));

                if (grSubjList.Contains((user as Teacher).Subject))
                    grListSort.Add(grName);
            }

            int posIncrement = 0;
            Button btn;
            foreach (string item in grListSort) {
                btn = new Button();
                btn.Location = new Point(20, 150 + posIncrement);
                btn.Height = 25;
                btn.Width = 100;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.Text = item;
                btn.Name = "DynamicButton" + posIncrement;
                btn.Font = new Font("Georgia", 10);
                btn.Click += new EventHandler(DynamicButton_Click);
                Controls.Add(btn);
                posIncrement += 30;
            }
        }

        private void DynamicButton_Click(object sender, EventArgs e) {
            MessageBox.Show("Dynamic button is clicked");
            // This must be a universal handler
        }

        private void button1_Click(object sender, EventArgs e) {
        }
    }
}