using System.Collections.Generic;

namespace Course_project {

    public interface ITestQuestion {
        int Value { get; set; }
        List<string> RightAns { get; set; }
        List<string> WrongAns { get; set; }
        string Question { get; set; }
        /*
        To get the type of question:
        if wrongAns.Count == 0 => detailed answer
        if rightAns.Count == 1 => only one right answer
        if rightAns.Count > 1 =>  multiple right answers
        */
    }
}