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
            string loginUsrname = Convert.ToString(textBox1.Text);
            string loginPass = Convert.ToString(textBox2.Text);
            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            while (reader.BaseStream.Position < reader.BaseStream.Length) {
                string usrname = reader.ReadString();
                string pass = reader.ReadString();
                string secrQ = reader.ReadString();
                string secrA = reader.ReadString();
                bool teacher = reader.ReadBoolean();
                string subj_gr = reader.ReadString();

                if (string.Compare(usrname, loginUsrname) == 0) {
                    if (string.Compare(pass, loginPass) == 0) {
                        if (teacher) {
                            Teacher t = new Teacher(usrname, pass, secrQ, secrA, subj_gr);
                            TeacherMainMenu tm = new TeacherMainMenu(t, this);
                            tm.Show();
                            textBox1.Clear();
                            textBox2.Clear();
                            reader.Close();
                            Hide();
                            return;
                        } else {
                            Student s = new Student(usrname, pass, secrQ, secrA, subj_gr);
                            StudentMainMenu sm = new StudentMainMenu(s, this);
                            sm.Show();
                            textBox1.Clear();
                            textBox2.Clear();
                            reader.Close();
                            Hide();
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
            Registration r = new Registration(this);
            r.Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e) { // Forgot password
            ForgotPass f = new ForgotPass(this);
            f.Show();
            Hide();
        }

        private void label1_Click(object sender, EventArgs e) {
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
        }
    }
}