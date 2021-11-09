using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    public class Test : ITest {

        public Test(string name, List<TestQuestion> questions, List<TestMark> studentMarksList, bool randQuestionOrder, int semester) {
            Name = name;
            Questions = questions;
            StudentMarksList = studentMarksList;
            RandQuestionOrder = randQuestionOrder;
            Semester = semester;
        }

        public Test(string name, List<TestQuestion> questions, bool randQuestionOrder, int semester) {
            Name = name;
            Questions = questions;
            StudentMarksList = new List<TestMark>();
            RandQuestionOrder = randQuestionOrder;
            Semester = semester;
        }

        public Test() {
            Name = null;
            Questions = new List<TestQuestion>();
            StudentMarksList = new List<TestMark>();
            RandQuestionOrder = false;
            Semester = 1;
        }

        public string Name { get; set; }
        public List<TestQuestion> Questions { get; set; }
        public List<TestMark> StudentMarksList { get; set; } // List of students who took this test
        public bool RandQuestionOrder { get; set; }
        public int Semester { get; set; }

        public int getStudentMark(string studentUsrName) {
            foreach (TestMark mark in StudentMarksList)
                if (string.Compare(mark.StudentUsrName, studentUsrName) == 0)
                    return mark.getStudentMark(maxScore());
            return 0;
        }

        public int maxScore() {
            int MaxScore = 0;
            foreach (TestQuestion question in Questions)
                MaxScore += question.Value;
            return MaxScore;
        }

        public void WriteToJson(string dir) {
            Directory.CreateDirectory(@dir);
            StreamWriter sw = new StreamWriter(File.Open(@dir + Name + ".json", FileMode.Create));
            string output = JsonConvert.SerializeObject(this);
            sw.WriteLine(output);
            sw.Close();
        }
    }
}