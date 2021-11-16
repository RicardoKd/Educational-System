using System;
using System.Collections.Generic;

namespace Course_project {

    public class TestMark : ITestMark {
        public string StudentUsrName { get; set; }
        public List<int> Marks { get; set; }
        public TimeSpan TimeSpent { get; set; }

        public int getStudentMark(int maxScore) {
            int totalScore = 0;
            foreach (int mark in Marks)
                totalScore += mark;
            totalScore = (totalScore * 100) / maxScore; // formula to covert to 100-point scale
            return totalScore;
        }
    }
}