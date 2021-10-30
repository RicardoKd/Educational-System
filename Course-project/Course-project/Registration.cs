using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Course_project {

    public partial class Registration : Form {
        private Form1 form1;

        public Registration(Form1 form1) {
            InitializeComponent();
            this.form1 = form1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Registration_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            string newUsrname = Convert.ToString(textBox1.Text);
            string newPass = Convert.ToString(textBox2.Text);
            string passCheck = Convert.ToString(textBox3.Text);
            string secrQ = Convert.ToString(comboBox5.Text);
            string secrA = Convert.ToString(textBox4.Text);

            if (string.IsNullOrEmpty(newUsrname) || // if main fields are empty
                string.IsNullOrEmpty(newPass) ||
                string.IsNullOrEmpty(passCheck) ||
                string.IsNullOrEmpty(secrA) ||
                (!radBtn1.Checked && !radBtn2.Checked) ||
                string.Compare(secrQ, "Secret question") == 0) {
                label4.Visible = true;
                return;
            }

            Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!(regex.IsMatch(newUsrname))) {
                MessageBox.Show("Username must contain at least 8 symbols, one digit, and one special symbol");
            }

            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            while (reader.BaseStream.Position < reader.BaseStream.Length) { // importing all users
                string username = reader.ReadString();
                if (string.Compare(username, newUsrname) == 0) { // if username taken
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

            if (!(regex.IsMatch(newPass))) { // pass validation
                MessageBox.Show("Password must contain at least 8 symbols, one digit, and one special symbol");
                label2.Visible = true;
                return;
            }

            if (!(string.Compare(newPass, passCheck) == 0)) { // password match
                label3.Visible = true;
                return;
            }

            string year = Convert.ToString(comboBox1.Text);
            string spec = Convert.ToString(comboBox2.Text);
            string subject = Convert.ToString(comboBox3.Text);

            if (radBtn1.Checked && string.IsNullOrEmpty(subject)) { // if variable fields are empty
                label4.Visible = true;
                return;
            }

            if (radBtn2.Checked && (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(spec))) { // if variable fields are empty
                label4.Visible = true;
                return;
            }

            try {
                BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.Append));
                writer.Write(newUsrname);
                writer.Write(newPass);
                writer.Write(secrQ);
                writer.Write(secrA);
                if (radBtn1.Checked) {
                    writer.Write(true); // true = teacher
                    writer.Write(subject);
                } else {
                    writer.Write(false); // false = student
                    string grName = spec + "." + year;
                    writer.Write(grName);

                    string dir = "Groups/" + grName + "/";
                    string fName = grName + ".txt";
                    string tracker = dir + newUsrname + "-tracker.txt";
                    if (Directory.Exists(@dir)) {
                        File.Create(tracker);
                        StreamWriter grWriter = new StreamWriter(File.Open(dir + fName, FileMode.Append));
                        grWriter.Write(", " + newUsrname);
                        grWriter.Close();
                    } else {
                        Directory.CreateDirectory(dir);
                        File.Create(tracker);
                        StreamWriter grWriter = new StreamWriter(File.Open(dir + fName, FileMode.OpenOrCreate));
                        grWriter.WriteLine(spec);
                        grWriter.WriteLine(year);
                        grWriter.Write(newUsrname);
                        grWriter.Close();

                        StreamWriter grListWriter = new StreamWriter(File.Open(@"groupList.txt", FileMode.Append));
                        grListWriter.Write(grName + " ");
                        grListWriter.Close();
                    }
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
            form1.Show();
            Close();
        }
    }
}