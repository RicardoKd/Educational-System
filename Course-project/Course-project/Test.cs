using System.Collections.Generic;

namespace Course_project {

    internal class Test {
        private List<TestQuestion> questions;
        private List<TestMark> marks;

        internal List<TestQuestion> Questions { get => questions; set => questions = value; }
        internal List<TestMark> Marks { get => marks; set => marks = value; }

        public int getStudentMark(string studentUsrName) {
            foreach (TestMark mark in Marks)
                if (string.Compare(mark.StudentUsrName, studentUsrName) == 0)
                    return mark.Mark;
            return 0;
        }
    }
}