using System.Collections.Generic;

namespace Course_project {

    public interface ITest {
        bool RandQuestionOrder { get; set; }
        int Semester { get; set; }
        List<TestMark> StudentMarksList { get; set; }
        List<TestQuestion> Questions { get; set; }
        string Name { get; set; }

        int getStudentMark(string studentUsrName);

        int maxScore();

        List<string> getQuestions();

        void WriteToJson(string dir);
    }
}