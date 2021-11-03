using System.Collections.Generic;

namespace Course_project {

    public class TestQuestion {
        private int value = 1; // relative weight of this question compared to others
        private string question;
        private List<string> rightAns;
        private List<string> wrongAns;
        /*
        To get the type of question:
        if wrongAns.Count == 0 => detailed answer
        if rightAns.Count == 1 => only one right answer
        if rightAns.Count > 1 =>  multiple right answers
        */

        public int Value { get => value; set => this.value = value; }
        public string Question { get => question; set => question = value; }
        public List<string> RightAns { get => rightAns; set => rightAns = value; }
        public List<string> WrongAns { get => wrongAns; set => wrongAns = value; }
    }
}