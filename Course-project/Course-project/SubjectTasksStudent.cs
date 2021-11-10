﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class SubjectTasksStudent : Form {
        public string Subject { get; set; }
        public string LectDir { get; set; } // with semester
        public string TestDir { get; set; } // with semester
        public StudentMainMenu StudentMainMenu { get; set; }

        public SubjectTasksStudent(string subject, StudentMainMenu studentMainMenu) {
            InitializeComponent();
            StudentMainMenu = studentMainMenu;
            Subject = subject;
            Group group = new Group(StudentMainMenu.Student.Group);
            int semester = Services.GetCurrentSemester();
            LectDir = "Lectures/" + group.Specialty + "/" + group.Year + "/" + Subject + "/" + semester + "/";
            TestDir = "Tests/" + group.Specialty + "/" + group.Year + "/" + Subject + "/" + semester + "/";
        }

        private void SubjectTasksStudent_Load(object sender, EventArgs e) {
            Services.fillDGV(dataGridView1, Services.getOrder(LectDir), "View"); // Fill lecture list
            Services.fillDGV(dataGridView2, Services.getOrder(TestDir), "Start"); // Fill test list
        }

        private void button1_Click(object sender, EventArgs e) {
            StudentMainMenu.Show();
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string lectName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(lectName)) {
                StreamReader r = new StreamReader(File.Open(LectDir + lectName + ".json", FileMode.Open));
                Lecture lect = JsonConvert.DeserializeObject<Lecture>(r.ReadToEnd());
                r.Close();
                /*ViewLecture sl = new ViewLecture(this, lect);
                sl.Show();
                Hide();*/
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            string testName = Services.DGVCellContentClick(sender, e, 1);
            if (!string.IsNullOrEmpty(testName)) {
                StreamReader r = new StreamReader(File.Open(TestDir + testName + ".json", FileMode.Open));
                Test test = JsonConvert.DeserializeObject<Test>(r.ReadToEnd());
                r.Close();
                /*ViewTest vt = new ViewTest(this, test, true);
                vt.Show();
                Hide();*/
            }
        }
    }
}