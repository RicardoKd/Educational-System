using System;
using System.Collections.Generic;
using System.Text;

namespace Course_project {

    public abstract class User {
        private string username;
        private string password;

        protected User(string username, string password) {
            this.username = username;
            this.password = password;
        }

        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }
    }
}