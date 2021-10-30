﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class TeacherMainMenu : Form {
        private User user;
        private Form1 form1;

        public TeacherMainMenu(User user, Form1 form1) {
            InitializeComponent();
            this.user = user;
            this.form1 = form1;
        }

        private void TeacherMainMenu_Load(object sender, EventArgs e) {
            label1.Text = (user as Teacher).Subject;
            label2.Text = "Hello, " + (user as Teacher).Username;
            if (File.Exists(@"groupList.txt")) {
                StreamReader grListReader = new StreamReader(File.Open(@"groupList.txt", FileMode.Open));
                List<string> grList = new List<string>(grListReader.ReadToEnd().Split(" ", StringSplitOptions.RemoveEmptyEntries));
                grListReader.Close();

                List<string> grListSort = new List<string>();
                foreach (string grName in grList) {
                    List<string> grSubjList = new List<string>(new Rules().getSubjList(grName));
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
        }

        private void DynamicButton_Click(object sender, EventArgs e) {
            string grName = Convert.ToString((sender as Button).Text); // get text of the btn that wa clicked
            GroupInfo grInfo = new GroupInfo(grName, this);
            grInfo.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e) {
        }

        private void button2_Click(object sender, EventArgs e) {
            form1.Show();
            Close();
        }
    }
}