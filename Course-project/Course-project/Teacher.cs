using System;
using System.Collections.Generic;
using System.Text;

namespace Course_project {

    public class Teacher : User {
        private string subject;

        public Teacher(string username, string password, string subject) : base(username, password) {
            this.subject = subject;
        }

        public string Subject { get => subject; set => subject = value; }
    }
}