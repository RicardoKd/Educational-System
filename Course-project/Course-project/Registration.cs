using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Course_project {

    public partial class Registration : Form {

        public Registration() {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            string NewUsername = Convert.ToString(textBox1.Text);
            string NewPassword = Convert.ToString(textBox2.Text);
            string passwordCheck = Convert.ToString(textBox3.Text);
            string secretAns = Convert.ToString(textBox4.Text);

            if (string.IsNullOrEmpty(NewUsername) || // if main fields are empty
                string.IsNullOrEmpty(NewPassword) ||
                string.IsNullOrEmpty(passwordCheck) ||
                string.IsNullOrEmpty(secretAns) ||
                (!radioButton1.Checked && !radioButton2.Checked) ||
                string.Compare(Convert.ToString(comboBox5.Text), "Secret question") == 0) {
                label4.Visible = true;
                return;
            }

            Regex regexChecker = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!(regexChecker.IsMatch(NewUsername))) {
                MessageBox.Show("Username must contain at least 8 symbols, one digit, and one special symbol");
            }

            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            while (reader.BaseStream.Position < reader.BaseStream.Length) { // importing all users
                string username = reader.ReadString();
                if (string.Compare(username, NewUsername) == 0) { // if username taken
                    label1.Visible = true;
                    reader.Close();
                    return;
                }
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadBoolean();
                reader.ReadString();
            }
            reader.Close();

            if (!(regexChecker.IsMatch(NewPassword))) { // pass validation
                MessageBox.Show("Password must contain at least 8 symbols, one digit, and one special symbol");
                label2.Visible = true;
                return;
            }

            if (!(string.Compare(NewPassword, passwordCheck) == 0)) { // password match
                label3.Visible = true;
                return;
            }

            if (radioButton1.Checked && string.IsNullOrEmpty(comboBox3.Text)) { // check if field "subject" is not empty
                label4.Visible = true;
                return;
            }

            if (radioButton2.Checked && (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))) { // check if fields "year" and "specialty" are not empty
                label4.Visible = true;
                return;
            }

            try {
                BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.Append));
                writer.Write(NewUsername);
                writer.Write(NewPassword);
                writer.Write(Convert.ToString(comboBox5.Text)); // secret question
                writer.Write(secretAns);
                if (radioButton1.Checked) {
                    writer.Write(true); // true = teacher
                    writer.Write(Convert.ToString(comboBox3.Text)); // subject
                } else {
                    writer.Write(false); // false = student
                    writer.Write(Convert.ToInt32(comboBox2.Text) + "." + Convert.ToInt32(comboBox1.Text)); // group name
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
            label9.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
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
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label9.Visible = true;
            comboBox3.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            label6.Visible = true;
            label7.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            label9.Visible = false;
            comboBox3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }
    }
}