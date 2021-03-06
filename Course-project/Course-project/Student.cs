namespace Course_project {

    public class Student : IUser {

        public Student(string username, string password, string secretQuestion, string secretAnswer, string group) {
            Group = group;
            Username = username;
            Password = password;
            SecretQuestion = secretQuestion;
            SecretAnswer = secretAnswer;
        }

        public string Group { get; set; }
        public string Password { get; set; }
        public string SecretAnswer { get; set; }
        public string SecretQuestion { get; set; }
        public string Username { get; set; }
    }
}