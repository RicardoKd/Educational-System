using System;
using System.Windows.Forms;

namespace Course_project {

    public partial class TeacherMainMenu : Form {
        private User user;

        public TeacherMainMenu(User user) {
            InitializeComponent();
            this.user = user;
        }

        private void TeacherMainMenu_Load(object sender, EventArgs e) {
            label1.Text = (user as Teacher).Subject;
            /*            int posIncrement = 0;
                        Button btn;
                        foreach (string item in (user as Teacher).StudentGroups) {
                            btn = new Button();
                            btn.Location = new Point(20, 150 + posIncrement);
                            btn.Height = 30;
                            btn.Width = 150;
                            btn.BackColor = Color.Red;
                            btn.ForeColor = Color.Blue;
                            btn.Text = "item";
                            btn.Name = "DynamicButton" + posIncrement;
                            btn.Font = new Font("Georgia", 16);
                            btn.Click += new EventHandler(DynamicButton_Click);
                            Controls.Add(btn);
                            posIncrement += 50;
                        }*/
        }

        private void DynamicButton_Click(object sender, EventArgs e) {
            MessageBox.Show("Dynamic button is clicked");
            // This must be a universal handler
        }

        private void button1_Click(object sender, EventArgs e) {
        }
    }
}