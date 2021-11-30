﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class AddTest : Form {
        public bool EditMode { get; set; }
        public bool SavedToOrder { get; set; } // true when this from is loaded for the first
        public GroupInfo GrInfoForm { get; set; }
        public string Dir { get; set; } // without semester
        public string OldName { get; set; }
        public Test Test { get; set; }

        public AddTest(GroupInfo grInfoForm) { // Constructor for creating new tests
            InitializeComponent();
            GrInfoForm = grInfoForm;
            Test = new Test();
            EditMode = false;
            SavedToOrder = false;
            Dir = "Tests/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            comboBox3.SelectedIndex = 0;
        }

        public AddTest(GroupInfo grInfoForm, Test test, bool savedToOrder = false) { // Constructor for editing tests
            InitializeComponent();
            GrInfoForm = grInfoForm;
            Test = test;
            SavedToOrder = savedToOrder;
            EditMode = true;
            OldName = test.Name;
            Dir = "Tests/" + grInfoForm.CurGr.Specialty + "/" + grInfoForm.CurGr.Year + "/" + grInfoForm.TeacherMM.Teacher.Subject + "/";
            textBox1.Text = test.Name;
            comboBox3.Text = Convert.ToString(test.Semester);
            checkBox1.Checked = test.RandQuestionOrder == true ? true : false;
            if (savedToOrder)
                Text = "Edit test";
            Services.fillDGV(dataGridView1, test.getQuestions(), "View");
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            GroupInfo grInfo = new GroupInfo(GrInfoForm.CurGr.Name, GrInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Save
            string name = textBox1.Text;
            bool randQOrder = checkBox1.Checked == true ? true : false;
            int newSemester = Convert.ToInt32(comboBox3.SelectedItem);
            if (string.IsNullOrEmpty(name)) {
                MessageBox.Show("Fill in test name field!");
                return;
            }

            if (EditMode && SavedToOrder) {
                List<string> testOrder = Services.getOrder(Dir + Test.Semester);
                int testInd = testOrder.FindIndex(x => x.Equals(OldName));
                File.Delete(Dir + Test.Semester + "/" + testOrder[testInd] + ".json"); // delete old version
                if (newSemester == Test.Semester) {
                    testOrder[testInd] = name;
                    Services.rewriteOrder(Dir + newSemester, testOrder);
                } else {
                    testOrder.RemoveAt(testInd); // delete the lecture because it's moved to another semester
                    Services.rewriteOrder(Dir + Test.Semester, testOrder);
                    Services.appendToOrder(Dir + newSemester, name);
                }
            } else
                Services.appendToOrder(Dir + newSemester, name);

            Test newTest = new Test(name, Test.Questions, randQOrder, newSemester);
            newTest.WriteToJson(Dir + newSemester);
            MessageBox.Show("The test is succesfuly saved!");
            GroupInfo grInfo = new GroupInfo(GrInfoForm.CurGr.Name, GrInfoForm.TeacherMM);
            grInfo.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e) { // Add question
            Add_Test_Question atq = new Add_Test_Question(this);
            atq.Show();
            Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { // "Edit" btn OnClick in question list
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) {
                string clickedQText = (string)senderGrid.Rows[e.RowIndex].Cells[1].Value;
                TestQuestion clickedQ = Test.Questions.Find(x => x.Question.Contains(clickedQText));
                Add_Test_Question tq = new Add_Test_Question(this, clickedQ);
                tq.Show();
                Hide();
            }
        }
    }
}