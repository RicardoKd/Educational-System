namespace Course_project {

    public class Teacher : User {
        private string subject;

        public Teacher(string username, string password, string secretQuestion, string secretAnswer, string subject) : base(username, password, secretQuestion, secretAnswer) {
            this.subject = subject;
        }

        public string Subject { get => subject; set => subject = value; }
    }
}