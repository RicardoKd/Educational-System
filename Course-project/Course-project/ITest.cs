using System.Collections.Generic;

namespace Course_project {

    public interface ITest {
        string Name { get; set; }
        List<TestQuestion> Questions { get; set; }
        bool RandQuestionOrder { get; set; }
        int Semester { get; set; }
        List<TestMark> StudentMarksList { get; set; }

        int getStudentMark(string studentUsrName);

        int maxScore();

        void WriteToJson(string dir);
    }
}