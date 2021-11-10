using System.Collections.Generic;

namespace Course_project {

    public interface ITestQuestion {
        string Question { get; set; }
        List<string> RightAns { get; set; }
        int Value { get; set; }

        List<string> WrongAns { get; set; }
        /*
        To get the type of question:
        if wrongAns.Count == 0 => detailed answer
        if rightAns.Count == 1 => only one right answer
        if rightAns.Count > 1 =>  multiple right answers
        */
    }
}