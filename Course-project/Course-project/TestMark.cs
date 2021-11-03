using System.Collections.Generic;

namespace Course_project {

    public class TestMark {
        private string studentUsrName;
        private List<int> marks;

        public string StudentUsrName { get => studentUsrName; set => studentUsrName = value; }
        public List<int> Marks { get => marks; set => marks = value; }

        public int getStudentMark(int maxScore) {
            int totalScore = 0;
            foreach (int mark in marks)
                totalScore += mark;
            totalScore = (totalScore * 100) / maxScore; // formula to covert to 100-point scale
            return totalScore;
        }
    }
}