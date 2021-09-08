using System;
using System.Collections.Generic;
using System.Text;

namespace Course_project {

    public class Student : User {
        private int year;
        private string specialty;
        private int group;
        // rukovoditel gruppi

        public Student(string username, string password, int year) : base(username, password) {
            this.year = year;
        }

        public int Year { get => year; set => year = value; }
        public string Specialty { get => specialty; set => specialty = value; }
        public int Group { get => group; set => group = value; }
    }
}