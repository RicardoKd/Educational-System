using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Course_project {

    public partial class Registration : Form {
        public List<User> users = new List<User>();

        public Registration() {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
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

            string username = Convert.ToString(textBox1.Text);
            string password = Convert.ToString(textBox2.Text);
            string passwordCheck = Convert.ToString(textBox3.Text);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordCheck)) {
                label4.Visible = true;
                return;
            }

            foreach (User user in users) { // username validation
                if (string.Compare(user.Username, username) == 0) {
                    label1.Visible = true;
                    return;
                }
            }

            Regex checkPass = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!(checkPass.IsMatch(password))) { // pass validation
                label2.Visible = true;
                return;
            }

            if (!(string.Compare(password, passwordCheck) == 0)) { // password match
                label3.Visible = true;
                return;
            }

            try {
                BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.OpenOrCreate));
                writer.Seek(0, SeekOrigin.End); // end of file
                writer.Write(username);
                writer.Write(password);
                writer.Write(Convert.ToString(comboBox5.Text)); // secret question
                writer.Write(Convert.ToString(textBox4.Text)); // secret answer

                if (radioButton1.Checked) {
                    writer.Write(true); // true = teacher
                    writer.Write(Convert.ToString(comboBox4.Text)); // subject
                } else {
                    writer.Write(false); // false = student
                    writer.Write(Convert.ToInt32(comboBox1.Text)); // year
                    writer.Write(Convert.ToInt32(comboBox2.Text)); // specialty
                    writer.Write(Convert.ToInt32(comboBox3.Text)); // group
                }

                writer.Close();
            } catch (Exception) {
                throw;
            }
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            button1.Visible = false;
            panel1.Visible = false;

            label10.Visible = true;
            button2.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            label1.Visible = false;
            label4.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            label2.Visible = false;
            label4.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            label3.Visible = false;
            label4.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = true;
            label9.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = false;
            label9.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }
    }
}