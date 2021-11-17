using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Course_project {

    internal class Services {

        public static int getRand(int start, int end) {
            Random random = new Random();
            int i = random.Next(start, end);
            return i;
        }

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

        public static void rewriteOrder(string dir, List<string> newOrder) {
            if (!dir.EndsWith("/"))
                dir += "/";
            StreamWriter wr = new StreamWriter(File.Open(dir + "order.txt", FileMode.Create));
            foreach (string lectName in newOrder)
                wr.Write(lectName + ",");
            wr.Close();
        }

        public static void appendToOrder(string dir, string nameToAppend) {
            if (!dir.EndsWith("/"))
                dir += "/";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            StreamWriter wr = new StreamWriter(File.Open(@dir + "order.txt", FileMode.Append));
            wr.Write(nameToAppend + ",");
            wr.Close();
        }

        public static void saveOrder(string dir, DataGridView dgv) {
            if (dgv.RowCount - 1 > 0) {
                StreamWriter wr = new StreamWriter(File.Open(dir + "order.txt", FileMode.Create));
                for (int i = 0; i < dgv.RowCount - 1; i++)
                    wr.Write((string)dgv.Rows[i].Cells[1].Value + ",");
                wr.Close();
            }
        }

        public delegate void btnOnClick(object sender, EventArgs e);

        public static List<Button> createBtnList(int x, int y, List<string> btnTextList, btnOnClick delFunc) {
            List<Button> btnList = new List<Button>();
            int posIncrement = 0;
            Button btn;
            foreach (string text in btnTextList) {
                btn = new Button();
                btn.Location = new Point(x, y + posIncrement);
                btn.Height = 30;
                btn.Width = 100;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.Text = text;
                btn.Name = "DynamicButton" + posIncrement;
                btn.Font = new Font("Georgia", 10);
                btn.Click += new EventHandler(delFunc);
                btnList.Add(btn);
                posIncrement += 30;
            }
            return btnList;
        }

        public static List<RadioButton> createRadioBtnList(int x, int y, List<string> btnTextList) {
            List<RadioButton> radBtnList = new List<RadioButton>();
            int posIncrement = 0;
            foreach (string text in btnTextList) {
                RadioButton radBtn = new RadioButton {
                    Location = new Point(x, y + posIncrement),
                    Text = text,
                    Name = "RadioBtn" + posIncrement,
                    ForeColor = Color.White
                };
                radBtnList.Add(radBtn);
                posIncrement += 20;
            }
            return radBtnList;
        }

        public static List<CheckBox> createChkBoxList(int x, int y, List<string> btnTextList) {
            List<CheckBox> radBtnList = new List<CheckBox>();
            int posIncrement = 0;
            foreach (string text in btnTextList) {
                CheckBox chkBox = new CheckBox {
                    Location = new Point(x, y + posIncrement),
                    Text = text,
                    Name = "CheckBox" + posIncrement,
                    ForeColor = Color.White
                };
                radBtnList.Add(chkBox);
                posIncrement += 20;
            }
            return radBtnList;
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

        public static T deserializeObj<T>(string filePath) {
            StreamReader r = new StreamReader(File.Open(filePath, FileMode.Open));
            T obj = JsonConvert.DeserializeObject<T>(r.ReadToEnd());
            r.Close();
            return obj;
        }

        public static void nextImg(ref int currentImg, Lecture lecture, PictureBox pcb) {
            if (currentImg == -1)
                return;
            if (currentImg == lecture.ImgList.Count - 1) {
                pcb.Image = Image.FromFile(lecture.ImgList[0]);
                currentImg = 0;
            } else {
                pcb.Image = Image.FromFile(lecture.ImgList[currentImg + 1]);
                currentImg++;
            }
        }

        public static void previousImg(ref int currentImg, Lecture lecture, PictureBox pcb) {
            if (currentImg == -1)
                return;
            if (currentImg == 0) {
                pcb.Image = Image.FromFile(lecture.ImgList[lecture.ImgList.Count - 1]);
                currentImg = lecture.ImgList.Count - 1;
            } else {
                pcb.Image = Image.FromFile(lecture.ImgList[currentImg - 1]);
                currentImg--;
            }
        }

        public static List<T> randomizeList<T>(List<T> listToRandomize) {
            List<T> initialCopy = new List<T>();
            foreach (T item in listToRandomize)
                initialCopy.Add(item);

            List<T> newOrder = new List<T>();
            int initialListLength = initialCopy.Count;
            for (int i = 0; i < initialListLength; i++) {
                int ind = getRand(0, initialCopy.Count);
                newOrder.Add(initialCopy[ind]);
                initialCopy.RemoveAt(ind);
            }
            return newOrder;
        }

        public static void nextQuestion(ViewTest ViewTest, RichTextBox rtb) {
            TestQuestion currentQ = ViewTest.NewOrder[ViewTest.QuestionInd];
            if (currentQ.WrongAns.Count == 0) { // detailed answer
                rtb.Visible = true;
            } else {
                List<string> rightWrong = new List<string>(currentQ.RightAns);
                rightWrong.AddRange(currentQ.WrongAns);
                List<string> randRightWrong = randomizeList(rightWrong);
                if (currentQ.RightAns.Count == 1) { // only one right answer
                    List<RadioButton> RadBtnList = createRadioBtnList(50, 130, randRightWrong);
                    foreach (RadioButton rb in RadBtnList)
                        ViewTest.Controls.Add(rb);
                } else if (currentQ.RightAns.Count > 1) { // multiple right answers
                    List<CheckBox> ChkBoxList = createChkBoxList(50, 130, randRightWrong);
                    foreach (CheckBox cb in ChkBoxList)
                        ViewTest.Controls.Add(cb);
                }
            }
        }

        public static List<T> getCollection<T>(Form Form) {
            return new List<T>(Form.Controls.OfType<T>());
        }

        public static TestMark derandomizeMarks(ViewTest vt, TimeSpan timeSpent) {
            TestMark rightMark = new TestMark {
                Marks = new List<int>(),
                TimeSpent = timeSpent,
                StudentUsrName = vt.SubjectTasksStudent.StudentMainMenu.Student.Username
            };
            foreach (TestQuestion item in vt.Test.Questions) {
                int newOrderInd = vt.NewOrder.FindIndex(x => x.Question.Equals(item.Question));
                rightMark.Marks.Add(vt.QuestionMarks[newOrderInd]);
            }
            return rightMark;
        }

        public static string DGVCellContentClick(object sender, DataGridViewCellEventArgs e, int colInd) {
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                return (string)senderGrid.Rows[e.RowIndex].Cells[colInd].Value;
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

        // For reordering rows
        public static void DGVMouseClick(DataGridView dgv, MouseEventArgs e, ref DataGridViewRow Rw, ref int RowIndexFromMouseDown) {
            if (dgv.SelectedRows.Count == 1)
                if (e.Button == MouseButtons.Left) {
                    Rw = dgv.SelectedRows[0];
                    RowIndexFromMouseDown = Rw.Index;
                    dgv.DoDragDrop(Rw, DragDropEffects.Move);
                }
        }

        public static void DGVDragEnter(DataGridView dgv, DragEventArgs e) {
            if (dgv.SelectedRows.Count > 0)
                e.Effect = DragDropEffects.Move;
        }

        public static void DGVragDrop(DataGridView dgv, DragEventArgs e, DataGridViewRow Rw, int RowIndexFromMouseDown) {
            Point clientPoint = dgv.PointToClient(new Point(e.X, e.Y));
            if (e.Effect == DragDropEffects.Move) {
                dgv.Rows.RemoveAt(RowIndexFromMouseDown);
                dgv.Rows.Insert(dgv.HitTest(clientPoint.X, clientPoint.Y).RowIndex, Rw);
            }
        }
    }
}