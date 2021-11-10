using System.Collections.Generic;

namespace Course_project {

    public class TestQuestion : ITestQuestion {
        public int Value { get; set; } = 1;
        public string Question { get; set; }
        public List<string> RightAns { get; set; }
        public List<string> WrongAns { get; set; }
    }
}