namespace Course_project {

    public class Student : User {
        private string group;

        public Student(string username, string password, string secretQuestion, string secretAnswer, string group) : base(username, password, secretQuestion, secretAnswer) {
            this.group = group;
        }

        public string Group { get => group; set => group = value; }
    }
}