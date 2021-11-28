using System;
using System.Windows.Forms;
using System.IO;

namespace Course_project {

    public partial class Registration : Form {
        public Form1 Form1 { get; set; }

        public Registration(Form1 form1) {
            InitializeComponent();
            Form1 = form1;
        }

        private void Registration_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            string usrName = Convert.ToString(textBox1.Text);
            string pass = Convert.ToString(textBox2.Text);
            string passCheck = Convert.ToString(textBox3.Text);
            string secrQ = Convert.ToString(comboBox5.Text);
            string secrA = Convert.ToString(textBox4.Text);
            string year = Convert.ToString(comboBox1.Text);
            string spec = Convert.ToString(comboBox2.Text);
            string subject = Convert.ToString(comboBox3.Text);
            if (string.IsNullOrEmpty(secrA) || (!radBtn1.Checked && !radBtn2.Checked)) {
                label4.Visible = true;
                return;
            }
            if (radBtn1.Checked && string.IsNullOrEmpty(subject)) { // if variable fields are empty
                label4.Visible = true;
                return;
            }
            if (radBtn2.Checked && (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(spec))) { // if variable fields are empty
                label4.Visible = true;
                return;
            }

            bool usrnameState = RegistrationServices.validateUsrName(usrName);
            bool passwordState = RegistrationServices.validatePass(pass);
            bool passMatchState = RegistrationServices.passMatch(pass, passCheck);

            if (!usrnameState || !passwordState || !passMatchState)
                return;

            try {
                BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.Append));
                writer.Write(usrName);
                writer.Write(pass);
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
                    if (Directory.Exists(@dir)) {
                        StreamWriter grWriter = new StreamWriter(File.Open(dir + fName, FileMode.Append));
                        grWriter.Write(", " + usrName);
                        grWriter.Close();
                    } else {
                        Directory.CreateDirectory(dir);
                        StreamWriter grWriter = new StreamWriter(File.Open(dir + fName, FileMode.OpenOrCreate));
                        grWriter.WriteLine(spec);
                        grWriter.WriteLine(year);
                        grWriter.Write(usrName);
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
            label4.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
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
            Form1.Show();
            Close();
        }
    }
}