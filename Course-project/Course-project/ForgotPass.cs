using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    public partial class ForgotPass : Form {
        public Form1 Form1 { get; set; }
        public List<IUser> Users { get; set; }
        public string CurrentUsrname { get; set; }
        public string SecrA { get; set; }

        public ForgotPass(Form1 form1) {
            InitializeComponent();
            Users = new List<IUser>();
            Form1 = form1;
        }

        private void button1_Click(object sender, EventArgs e) { // reset button
            if (string.Compare(Convert.ToString(textBox2.Text), SecrA) != 0) { // secret answer comparison
                label4.Visible = true;
                return;
            }
            string newPass = Convert.ToString(textBox3.Text);
            bool passwordState = RegistrationServices.validatePass(newPass);
            if (!passwordState) {
                label5.Visible = true;
                return;
            }
            foreach (IUser user in Users)
                if (string.Compare(user.Username, CurrentUsrname) == 0)
                    user.Password = newPass;
            // Change the binary file
            foreach (IUser user in Users)
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
            label1.Visible = false;
            label3.Visible = false;
            button1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e) { // Go back button
            Form1.Show();
            Close();
        }

        private void button3_Click(object sender, EventArgs e) { // next button
            CurrentUsrname = Convert.ToString(textBox1.Text);
            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            bool found = false;
            string secrQ = "";
            while (reader.BaseStream.Position < reader.BaseStream.Length) { // importing all users
                string usrname = reader.ReadString();
                string pass = reader.ReadString();
                secrQ = reader.ReadString();
                SecrA = reader.ReadString();
                bool teacher = reader.ReadBoolean();
                string subj_gr = reader.ReadString();
                IUser u;
                if (teacher)
                    u = new Teacher(usrname, pass, secrQ, SecrA, subj_gr);
                else
                    u = new Student(usrname, pass, secrQ, SecrA, subj_gr);
                Users.Add(u);

                if (string.Compare(usrname, CurrentUsrname) == 0) // if username found
                    found = true;
            }
            reader.Close();
            if (found) {
                textBox1.Enabled = false;
                button3.Visible = false;
                textBox2.Visible = true;
                textBox3.Visible = true;
                button1.Visible = true;
                label3.Text = secrQ;
                label3.Visible = true;
                return;
            } else {
                MessageBox.Show("No such username!");
                return;
            }
        }
    }
}