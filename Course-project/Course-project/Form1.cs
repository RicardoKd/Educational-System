using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
        }

        // all users
        public List<User> users = new List<User>();

        private void button1_Click(object sender, EventArgs e) { // Log in
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
            string loginPassword = Convert.ToString(textBox2.Text);

            foreach (User user in users)
                if (string.Compare(user.Username, loginUserName) == 0) {
                    if (string.Compare(user.Password, loginPassword) == 0) {
                        MainMenu mm = new MainMenu(user);
                        mm.Show();
                        return;
                    } else {
                        MessageBox.Show("Incorrect password!");
                        return;
                    }
                }

            MessageBox.Show("Username not found!");

            foreach (User user in users) {
                if (string.Compare(user.Username, loginUserName) == 0 && string.Compare(user.Password, loginPassword) == 0) {
                    MainMenu mm = new MainMenu(user);
                    mm.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) { // Registration
            Registration r = new Registration();
            r.Show();
        }

        private void button3_Click(object sender, EventArgs e) { // Forgot password
            ForgotPass f = new ForgotPass();
            f.Show();
        }

        private void label1_Click(object sender, EventArgs e) {
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
        }
    }
}