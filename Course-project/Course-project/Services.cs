using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Course_project {

    internal class Services {

        public static int GetCurrentSemester() {
            int semester = DateTime.Now.Month > 6 ? 1 : 2;
            return semester;
        }

        public static List<string> getOrder(string dir) {
            if (!Directory.Exists(dir))
                return new List<string>();
            if (!dir.EndsWith("/"))
                dir += "/";

            StreamReader r = new StreamReader(File.Open(@dir + "order.txt", FileMode.Open));
            List<string> order = new List<string>(r.ReadToEnd().Split(",", StringSplitOptions.RemoveEmptyEntries));
            r.Close();
            return order;
        }

        public delegate void btnOnClick(object sender, EventArgs e);

        public static List<Button> createBtnList(int x, int y, List<string> btnTextList, btnOnClick delFunc) {
            List<Button> btnList = new List<Button>();
            int posIncrement = 0;
            Button btn;
            foreach (string item in btnTextList) {
                btn = new Button();
                btn.Location = new Point(x, y + posIncrement);
                btn.Height = 30;
                btn.Width = 100;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.Text = item;
                btn.Name = "DynamicButton" + posIncrement;
                btn.Font = new Font("Georgia", 10);
                btn.Click += new EventHandler(delFunc);
                btnList.Add(btn);
                posIncrement += 30;
            }
            return btnList;
        }

        public static List<string> getGroupList() {
            if (File.Exists(@"groupList.txt")) {
                StreamReader grListReader = new StreamReader(File.Open(@"groupList.txt", FileMode.Open));
                List<string> grList = new List<string>(grListReader.ReadToEnd().Split(" ", StringSplitOptions.RemoveEmptyEntries));
                grListReader.Close();
                return grList;
            }
            return null;
        }

        public static List<string> getGroupsWithSubj(string subject) {
            List<string> grList = getGroupList();
            if (grList.Count > 0) {
                List<string> grListSort = new List<string>();
                foreach (string grName in grList) {
                    List<string> grSubjList = Rules.getSubjList(grName);
                    if (grSubjList.Contains(subject))
                        grListSort.Add(grName);
                }
                return grListSort;
            }
            return null;
        }

        public static void fillDGV(DataGridView dgw, List<string> nameList, string btnText) {
            int i = 0;
            foreach (string name in nameList) {
                dgw.Rows.Add();
                dgw.Rows[i].Cells[0].Value = i + 1;
                dgw.Rows[i].Cells[1].Value = name;
                dgw.Rows[i].Cells[2].Value = btnText;
                i++;
            }
        }

        public static string DGVCellContentClick(object sender, DataGridViewCellEventArgs e, int colInd) {
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                return (string)senderGrid.Rows[e.RowIndex].Cells[colInd].Value;
            return null;
        }
    }
}