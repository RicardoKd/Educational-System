using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Course_project {

    public partial class ForgotPass : Form {
        private string currentUsername;
        private string secretAnswer;
        private List<User> users = new List<User>();

        public ForgotPass() {
            InitializeComponent();
        }

        private void ForgotPass_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) { // reset button
            if (string.Compare(Convert.ToString(textBox2.Text), secretAnswer) != 0) { // secret answer comparison
                label4.Visible = true;
                return;
            }

            Regex regexChecker = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            string newPassword = Convert.ToString(textBox3.Text);
            if (!(regexChecker.IsMatch(newPassword))) { // new pass validation
                label5.Visible = true;
                return;
            }

            foreach (User user in users)
                if (string.Compare(user.Username, currentUsername) == 0)
                    user.Password = newPassword;

            // Change the binary file
            foreach (User user in users) {
                try {
                    BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.Truncate));
                    writer.Write(user.Username);
                    writer.Write(user.Password);
                    writer.Write(user.SecretQuestion);
                    writer.Write(user.SecretAnswer);
                    if (user is Teacher) {
                        writer.Write(true); // true = teacher
                        writer.Write((user as Teacher).Subject);
                    } else {
                        writer.Write(false); // false = student
                        writer.Write((user as Student).Group);
                    }
                    writer.Close();
                } catch (Exception) {
                    throw;
                }
            }
            label1.Visible = false;
            label3.Visible = false;
            button1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label2.Visible = true;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e) { // Go back button
            Close();
        }

        private void button3_Click(object sender, EventArgs e) { // next button
            currentUsername = Convert.ToString(textBox1.Text);
            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            bool found = false;
            string secretQuestion = "";
            while (reader.BaseStream.Position < reader.BaseStream.Length) { // importing all users
                string username = reader.ReadString();
                string password = reader.ReadString();
                secretQuestion = reader.ReadString();
                secretAnswer = reader.ReadString();
                bool teacher = reader.ReadBoolean();
                string subject_group = reader.ReadString();
                User u;
                if (teacher)
                    u = new Teacher(username, password, secretQuestion, secretAnswer, subject_group);
                else
                    u = new Student(username, password, secretQuestion, secretAnswer, subject_group);
                users.Add(u);

                if (string.Compare(username, currentUsername) == 0) // if username found
                    found = true;
            }
            reader.Close();

            if (found) {
                textBox1.Enabled = false;
                button3.Visible = false;
                textBox2.Visible = true;
                textBox3.Visible = true;
                button1.Visible = true;
                label3.Text = secretQuestion;
                label3.Visible = true;
                return;
            } else {
                MessageBox.Show("No such username!");
                return;
            }
        }
    }
}