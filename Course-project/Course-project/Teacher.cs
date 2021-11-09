namespace Course_project {

    public class Teacher : IUser {

        public Teacher(string username, string password, string secretQuestion, string secretAnswer, string subject) {
            Subject = subject;
            Username = username;
            Password = password;
            SecretQuestion = secretQuestion;
            SecretAnswer = secretAnswer;
        }

        public string Subject { get; set; }
        public string Password { get; set; }
        public string SecretAnswer { get; set; }
        public string SecretQuestion { get; set; }
        public string Username { get; set; }
    }
}