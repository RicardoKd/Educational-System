using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    public class Test {
        private string name;
        private List<TestQuestion> questions;
        private List<TestMark> studentMarksList; // List of students who took this test
        private bool randQuestionOrder;
        private bool randAnswerOrder;
        private int semester;

        public Test(string name, List<TestQuestion> questions, List<TestMark> studentMarksList, bool randQuestionOrder, bool randAnswerOrder, int semester) {
            this.name = name;
            this.questions = questions;
            this.studentMarksList = studentMarksList;
            this.randQuestionOrder = randQuestionOrder;
            this.randAnswerOrder = randAnswerOrder;
            this.semester = semester;
        }

        public Test(string name, List<TestQuestion> questions, bool randQuestionOrder, bool randAnswerOrder, int semester) {
            this.name = name;
            this.questions = questions;
            this.studentMarksList = new List<TestMark>();
            this.randQuestionOrder = randQuestionOrder;
            this.randAnswerOrder = randAnswerOrder;
            this.semester = semester;
        }

        public Test() {
            name = null;
            questions = new List<TestQuestion>();
            studentMarksList = new List<TestMark>();
            randQuestionOrder = false;
            randAnswerOrder = false;
            semester = 1;
        }

        public string Name { get => name; set => name = value; }
        public List<TestQuestion> Questions { get => questions; set => questions = value; }
        public List<TestMark> StudentMarksList { get => studentMarksList; set => studentMarksList = value; }
        public bool RandQuestionOrder { get => randQuestionOrder; set => randQuestionOrder = value; }
        public bool RandAnswerOrder { get => randAnswerOrder; set => randAnswerOrder = value; }
        public int Semester { get => semester; set => semester = value; }

        public int getStudentMark(string studentUsrName) {
            foreach (TestMark mark in studentMarksList)
                if (string.Compare(mark.StudentUsrName, studentUsrName) == 0)
                    return mark.getStudentMark(maxScore());
            return 0;
        }

        public int maxScore() {
            int MaxScore = 0;
            foreach (TestQuestion question in questions)
                MaxScore += question.Value;
            return MaxScore;
        }

        public void WriteToJson(string dir) {
            Directory.CreateDirectory(@dir);
            StreamWriter sw = new StreamWriter(File.Open(@dir + name + ".json", FileMode.Create));
            string output = JsonConvert.SerializeObject(this);
            sw.WriteLine(output);
            sw.Close();
        }
    }
}