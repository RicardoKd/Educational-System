using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Course_project {

    public partial class MainMenu : Form {
        public static User user;

        public MainMenu(User user) {
            InitializeComponent();
            MainMenu.user = user;
        }

        private void MainMenu_Load(object sender, EventArgs e) {
            label1.Text = user.Username;
        }
    }
}