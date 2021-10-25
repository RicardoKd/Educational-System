using System;
using System.Collections;
using System.Windows.Forms;

namespace Lab_5 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private Insurance insurance;

        private void Form1_Load(object sender, EventArgs e) {
            insurance = new Insurance();
            Insurance.F = false;
        }

        public void ViewEvent(object sender, EventArgs e) {
            textBox14.Text = Convert.ToString(insurance.Turnover);
        }

        private void button1_Click(object sender, EventArgs e) {
        }

        private void button2_Click(object sender, EventArgs e) {
        }

        private void button3_Click(object sender, EventArgs e) {
        }

        private void groupBox2_Enter(object sender, EventArgs e) {
        }

        private void button4_Click(object sender, EventArgs e) {
            if (!Insurance.F) {
                Insurance.F = true;
                insurance.Name = Convert.ToString(textBox11.Text);
                insurance.YearlyIncome = Convert.ToInt32(textBox12.Text);
                insurance.Workers = Convert.ToInt32(textBox13.Text);
                ITurnover IT = (ITurnover)insurance;
                insurance.Turnover = IT.Turnover();
                textBox14.Text = Convert.ToString(insurance.Turnover);
            } else
                MessageBox.Show("Only one insurance company is allowed to exist");
        }

        private void button5_Click(object sender, EventArgs e) {
            if (Insurance.F) {
                Factory factory = new Factory();
                factory.AddFactory += new EventHandler(insurance.DoEvent);
                factory.AddFactory += new EventHandler(ViewEvent);
                factory.Name = Convert.ToString(textBox15.Text);
                factory.YearlyIncome = Convert.ToInt32(textBox16.Text);
                factory.Workers = Convert.ToInt32(textBox17.Text);
                ITurnover IT = (ITurnover)factory;
                factory.Turnover = IT.Turnover();
                insurance.Clients.Add(factory);

                textBox18.Text += factory.Name + ". Income: " + factory.Turnover + ". Workers: " +
                factory.Workers + "\n";
            } else
                MessageBox.Show("There is no insurance company!");
        }
    }
}