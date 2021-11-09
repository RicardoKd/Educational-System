using System.Windows.Forms;

namespace Mouse_1 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            MessageBox.Show(e.KeyData.ToString());
        }
    }
}