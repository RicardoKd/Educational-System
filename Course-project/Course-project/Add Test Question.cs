using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class Add_Test_Question : Form {
        public AddTest AddTestForm { get; set; }
        public string OldQText { get; set; }
        public bool EditMode { get; set; }

        public Add_Test_Question(AddTest addTestForm) { // Constructor for creating new questions
            InitializeComponent();
            AddTestForm = addTestForm;
            EditMode = false;
            comboBox1.SelectedIndex = 0;
        }

        public Add_Test_Question(AddTest addTestForm, TestQuestion question) { // Constructor for editing questions
            InitializeComponent();
            Text = "Edit question";
            AddTestForm = addTestForm;
            EditMode = true;
            OldQText = question.Question;
            textBox1.Text = question.Question;
            comboBox1.SelectedIndex = question.Value - 1;
            foreach (string ra in question.RightAns)
                richTextBox1.Text += ra + "\n";
            foreach (string wa in question.WrongAns)
                richTextBox2.Text += wa + "\n";
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            AddTest at = new AddTest(AddTestForm.GrInfoForm, AddTestForm.Test, AddTestForm.SavedToOrder);
            at.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Save
            if (string.IsNullOrEmpty(textBox1.Text) || (string.IsNullOrEmpty(richTextBox1.Text) && string.IsNullOrEmpty(richTextBox1.Text))) {
                MessageBox.Show("Fill in all cells!");
                return;
            }
            TestQuestion tq = new TestQuestion {
                Question = textBox1.Text,
                Value = Convert.ToInt32(comboBox1.SelectedItem),
                RightAns = new List<string>(Convert.ToString(richTextBox1.Text).Split("\n", StringSplitOptions.RemoveEmptyEntries)),
                WrongAns = new List<string>(Convert.ToString(richTextBox2.Text).Split("\n", StringSplitOptions.RemoveEmptyEntries))
            };
            if (EditMode) {
                for (int i = 0; i < AddTestForm.Test.Questions.Count; i++)
                    if (string.Compare(OldQText, AddTestForm.Test.Questions[i].Question) == 0) {
                        AddTestForm.Test.Questions[i] = tq;
                        break;
                    }
            } else
                AddTestForm.Test.Questions.Add(tq);
            MessageBox.Show("Question is succesfuly saved!");
            AddTest at = new AddTest(AddTestForm.GrInfoForm, AddTestForm.Test, AddTestForm.SavedToOrder);
            at.Show();
            Close();
        }
    }
}