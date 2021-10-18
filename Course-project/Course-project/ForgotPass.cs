using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Course_project {

    public partial class ForgotPass : Form {
        public List<User> users = new List<User>();
        private string currentUserName;

        public ForgotPass() {
            InitializeComponent();
        }

        private void ForgotPass_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) { // reset button
            foreach (User user in users) {
                if (string.Compare(user.Username, currentUserName) == 0) {
                    string newPassword = Convert.ToString(textBox3.Text);
                    Regex checkPass = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

                    if (string.Compare(Convert.ToString(textBox2.Text), user.SecretAnswer) != 0) { // secret answer comparison
                        label4.Visible = true;
                        return;
                    }
                    if (!(checkPass.IsMatch(newPassword))) { // new pass validation
                        label5.Visible = true;
                        return;
                    }
                    user.Password = newPassword;
                }
            }

            // Change the binary file
            foreach (User user in users) {
                try {
                    if (File.Exists(@"users.txt"))
                        File.Delete(@"users.txt");
                    BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.OpenOrCreate));
                    writer.Seek(0, SeekOrigin.End);
                    writer.Write(user.Username);
                    writer.Write(user.Password);
                    writer.Write(user.SecretQuestion);
                    writer.Write(user.SecretAnswer);

                    if (user is Teacher) {
                        writer.Write(true); // true = teacher
                        writer.Write((user as Teacher).Subject);
                    } else {
                        writer.Write(false); // false = student
                        writer.Write((user as Student).Year);
                        writer.Write((user as Student).Specialty);
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
            users.Clear();
            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.Open));
            while (reader.BaseStream.Position < reader.BaseStream.Length) {
                string usrNm = reader.ReadString();
                string pass = reader.ReadString();
                string secretQuestion = reader.ReadString();
                string secrAnswer = reader.ReadString();
                bool teacher = reader.ReadBoolean();
                if (teacher) {
                    string subject = reader.ReadString();
                    Teacher t = new Teacher(usrNm, pass, secretQuestion, secrAnswer, subject);
                    users.Add(t);
                } else {
                    int year = reader.ReadInt32();
                    int specialty = reader.ReadInt32();
                    int group = reader.ReadInt32();
                    Student s = new Student(usrNm, pass, secretQuestion, secrAnswer, year, specialty, group);
                    users.Add(s);
                }
            }
            reader.Close();

            string loginUserName = Convert.ToString(textBox1.Text);

            foreach (User user in users)
                if (string.Compare(user.Username, loginUserName) == 0) {
                    currentUserName = user.Username; // remember username
                    textBox1.Enabled = false;
                    button3.Visible = false;
                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    button1.Visible = true;
                    label3.Text = user.SecretQuestion;
                    label3.Visible = true;
                    return;
                }
            MessageBox.Show("Username not found!");
        }
    }
}