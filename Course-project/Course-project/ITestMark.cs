using System;
using System.Collections.Generic;

namespace Course_project {

    public interface ITestMark {
        List<int> Marks { get; set; }
        string StudentUsrName { get; set; }
        TimeSpan TimeSpent { get; set; }

        int getStudentMark(int maxScore);
    }
}