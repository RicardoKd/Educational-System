using System;
using System.Collections.Generic;

namespace Course_project {

    internal class Group {
        private string name;
        private string specialty;
        private string year;
        private List<string> students;
        private List<string> teachers;

        public Group(string specialty, string year) {
            this.specialty = specialty;
            this.year = year;
            name = specialty + "." + year;
            students = new List<string>();
            teachers = new List<string>();
            // Fill teachers[] with usernames according to Rules.txt
        }

        public string Name { get => name; set => name = value; }
        public string Specialty { get => specialty; set => specialty = value; }
        public string Year { get => year; set => year = value; }
        public List<string> Students { get => students; set => students = value; }
        public List<string> Teachers { get => teachers; set => teachers = value; }

        public bool addStudent(string username) {
            if (!students.Contains(username)) {
                students.Add(username);
                return true;
            }
            return false;
        }

        public bool rmStudent(string username) {
            if (students.Contains(username)) {
                students.Remove(username);
                return true;
            }
            return false;
        }

        public bool addTeacher(string username) {
            if (!teachers.Contains(username)) {
                teachers.Add(username);
                return true;
            }
            return false;
        }

        public bool rmTeacher(string username) {
            if (teachers.Contains(username)) {
                teachers.Remove(username);
                return true;
            }
            return false;
        }
    }
}