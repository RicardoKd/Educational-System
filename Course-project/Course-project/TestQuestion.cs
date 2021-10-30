using System.Collections.Generic;

namespace Course_project {

    internal class TestQuestion {
        private List<string> rightAns;
        /*
        To get the type of question:
        if wrongAns.Count == 0 => detailed answer
        if rightAns.Count == 1 => only one right answer
        if rightAns.Count > 1 =>  multiple right answers
        */
        private List<string> wrongAns;

        public List<string> RightAns { get => rightAns; set => rightAns = value; }
        public List<string> WrongAns { get => wrongAns; set => wrongAns = value; }
    }
}