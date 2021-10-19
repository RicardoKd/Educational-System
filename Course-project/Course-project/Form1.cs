using System;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) { // Log in
            string loginUsername = Convert.ToString(textBox1.Text);
            string loginPassword = Convert.ToString(textBox2.Text);
            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            while (reader.BaseStream.Position < reader.BaseStream.Length) {
                string username = reader.ReadString();
                string password = reader.ReadString();
                string secretQuestion = reader.ReadString();
                string secrAnswer = reader.ReadString();
                bool teacher = reader.ReadBoolean();
                string subject_group = reader.ReadString();

                if (string.Compare(username, loginUsername) == 0) {
                    if (string.Compare(password, loginPassword) == 0) {
                        if (teacher) {
                            Teacher t = new Teacher(username, password, secretQuestion, secrAnswer, subject_group);
                            TeacherMainMenu tm = new TeacherMainMenu(t);
                            tm.Show();
                            textBox1.Clear();
                            textBox2.Clear();
                            reader.Close();
                            return;
                        } else {
                            Student s = new Student(username, password, secretQuestion, secrAnswer, subject_group);
                            StudentMainMenu sm = new StudentMainMenu(s);
                            sm.Show();
                            textBox1.Clear();
                            textBox2.Clear();
                            reader.Close();
                            return;
                        }
                    } else {
                        MessageBox.Show("Incorrect password!");
                        reader.Close();
                        return;
                    }
                }
            }
            reader.Close();
            MessageBox.Show("Username not found!");
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