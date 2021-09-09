using System;
using System.Collections.Generic;
using System.Text;

namespace Course_project {

    public abstract class User {
        private string username;
        private string password;
        private string secretQuestion;
        private string secretAnswer;

        public User(string username, string password, string secretQuestion, string secretAnswer) {
            this.username = username;
            this.password = password;
            this.secretQuestion = secretQuestion;
            this.secretAnswer = secretAnswer;
        }

        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }
        public string SecretQuestion { get => secretQuestion; set => secretQuestion = value; }
        public string SecretAnswer { get => secretAnswer; set => secretAnswer = value; }
    }
}