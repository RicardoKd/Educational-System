using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Course_project {

    internal class RegistrationServices {
        private static Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        public static bool validateUsrName(string usrName) {
            if (string.IsNullOrEmpty(usrName) || !regex.IsMatch(usrName)) {
                MessageBox.Show("Username must contain at least 8 symbols, one digit, and one special symbol", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            BinaryReader reader = new BinaryReader(File.Open(@"users.txt", FileMode.OpenOrCreate));
            while (reader.BaseStream.Position < reader.BaseStream.Length) { // importing all users
                string username = reader.ReadString();
                if (string.Compare(username, usrName) == 0) { // if username taken
                    MessageBox.Show("Username taken!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    reader.Close();
                    return false;
                }
                reader.ReadString();
                reader.ReadString();
                reader.ReadString();
                reader.ReadBoolean();
                reader.ReadString();
            }
            reader.Close();

            return true;
        }

        public static bool validatePass(string password) {
            if (string.IsNullOrEmpty(password) || !regex.IsMatch(password)) {
                MessageBox.Show("Password must contain at least 8 symbols, one digit, and one special symbol", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool passMatch(string password, string repeatPass) {
            if (string.Compare(password, repeatPass) != 0) {
                MessageBox.Show("Passwords doesn't match!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}