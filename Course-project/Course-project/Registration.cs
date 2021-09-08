﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Course_project {

    public partial class Registration : Form {
        public static List<User> users = new List<User>();

        public Registration(List<User> userslst) {
            InitializeComponent();
            users.AddRange(userslst);
        }

        private void Registration_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
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
            if (!(checkPass.IsMatch(password))) { // Pass validation
                label2.Visible = true;
                return;
            }

            if (!(string.Compare(password, passwordCheck) == 0)) { // Passwords don't match
                label3.Visible = true;
                return;
            }

            try {
                BinaryWriter writer = new BinaryWriter(File.Open(@"users.txt", FileMode.OpenOrCreate));
                writer.Seek(0, SeekOrigin.End); // end of file
                writer.Write(username);
                writer.Write(password);
                if (radioButton1.Checked)
                    writer.Write(true); // true = teacher
                else
                    writer.Write(false); // false = student

                writer.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
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
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            button1.Visible = false;
            groupBox1.Visible = false;

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