using System;
using System.Collections.Generic;

namespace Course_project {

    public class TestMark : ITestMark {
        public string StudentUsrName { get; set; }
        public List<int> Marks { get; set; }
        public TimeSpan TimeSpent { get; set; }

        public TestMark() {
            StudentUsrName = null;
            Marks = new List<int>();
            TimeSpent = TimeSpan.Zero;
        }

        public TestMark(string studentUsrName, List<int> marks, TimeSpan timeSpent) {
            StudentUsrName = studentUsrName;
            Marks = new List<int>(marks);
            TimeSpent = timeSpent;
        }

        public int getStudentMark(int maxScore) {
            int totalScore = 0;
            foreach (int mark in Marks)
                totalScore += mark;
            totalScore = (totalScore * 100) / maxScore; // formula to covert to 100-point scale
            return totalScore;
        }
    }
}