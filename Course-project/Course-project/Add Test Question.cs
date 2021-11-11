using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Course_project {

    public partial class Add_Test_Question : Form {
        public AddTest AddTestForm { get; set; }
        public string OldQText { get; set; }
        public bool EditMode { get; set; }
        public bool Changed { get; set; }

        // Constructor for creating new questions
        public Add_Test_Question(AddTest addTestForm) {
            InitializeComponent();
            AddTestForm = addTestForm;
            EditMode = false;
            comboBox1.SelectedIndex = 0;
        }

        // Constructor for editing questions
        public Add_Test_Question(AddTest addTestForm, TestQuestion question) {
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

        private void Add_Test_Question_Load(object sender, EventArgs e) {
            FormClosing += new FormClosingEventHandler(beforeClosing);
            Changed = false;
        }

        private void button1_Click(object sender, EventArgs e) { // Back
            AddTest at = new AddTest(AddTestForm.GrInfoForm, AddTestForm.Test, AddTestForm.SavedToOrder);
            at.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e) { // Save
            // might contain errors
            // make the checking more specific
            if (string.IsNullOrEmpty(textBox1.Text) || (string.IsNullOrEmpty(richTextBox1.Text) && string.IsNullOrEmpty(richTextBox1.Text))) {
                MessageBox.Show("Fill in all cells!");
                return;
            }

            TestQuestion tq = new TestQuestion();
            tq.Question = textBox1.Text;
            tq.Value = Convert.ToInt32(comboBox1.SelectedItem);
            tq.RightAns = new List<string>(Convert.ToString(richTextBox1.Text).Split("\n", StringSplitOptions.RemoveEmptyEntries));
            tq.WrongAns = new List<string>(Convert.ToString(richTextBox2.Text).Split("\n", StringSplitOptions.RemoveEmptyEntries));
            if (EditMode) {
                for (int i = 0; i < AddTestForm.Test.Questions.Count; i++)
                    if (string.Compare(OldQText, AddTestForm.Test.Questions[i].Question) == 0) {
                        AddTestForm.Test.Questions[i] = tq;
                        break;
                    }
            } else {
                AddTestForm.Test.Questions.Add(tq);
            }
            Changed = false;
            MessageBox.Show("Question is succesfuly saved!");
        }

        private void beforeClosing(object sender, FormClosingEventArgs e) {
            /*if (changed) {
                DialogResult dr = MessageBox.Show("Data is not saved. Want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) {
                    e.Cancel = true;
                    return;
                }
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            Changed = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            Changed = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            Changed = true;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e) {
            Changed = true;
        }
    }
}