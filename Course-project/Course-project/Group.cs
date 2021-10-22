using System.Collections.Generic;

namespace Course_project {

    internal class Group {
        private string name;
        private string specialty;
        private string year;
        private List<string> students;
        private List<string> subjects;

        public Group(string specialty, string year) {
            this.specialty = specialty;
            this.year = year;
            name = specialty + "." + year;
            students = new List<string>();
            subjects = new List<string>();
            subjects.AddRange(new Rules().getSubjList(specialty, year));
        }

        public string Name { get => name; set => name = value; }
        public string Specialty { get => specialty; set => specialty = value; }
        public string Year { get => year; set => year = value; }
        public List<string> Students { get => students; set => students = value; }
        public List<string> Subjects { get => subjects; set => subjects = value; }

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
    }
}