using System;
using System.Collections.Generic;
using System.Text;

namespace Course_project {

    public class Student : User {
        private int year;
        private int specialty;
        private int group;
        // rukovoditel gruppi

        public Student(string username, string password, string secretQuestion, string secretAnswer, int year, int specialty, int group) : base(username, password, secretQuestion, secretAnswer) {
            this.year = year;
            this.specialty = specialty;
            this.group = group;
        }

        public int Year { get => year; set => year = value; }
        public int Specialty { get => specialty; set => specialty = value; }
        public int Group { get => group; set => group = value; }
    }
}