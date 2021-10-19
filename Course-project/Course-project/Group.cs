using System;

namespace Course_project {

    internal class Group {
        private string name;
        private int specialty;
        private int year;
        private string[] students;
        private string[] teachers;

        public Group(int specialty, int year) {
            this.specialty = specialty;
            this.year = year;
            name = Convert.ToString(specialty) + "." + Convert.ToString(year);
            // Fill teachers[] with usernames according to Rules.txt
        }

        public string Name { get => name; set => name = value; }
        public int Specialty { get => specialty; set => specialty = value; }
        public int Year { get => year; set => year = value; }
        public string[] Students { get => students; set => students = value; }
        public string[] Teachers { get => teachers; set => teachers = value; }
    }
}