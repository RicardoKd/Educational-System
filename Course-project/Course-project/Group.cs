using System;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    public class Group {
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
            subjects = new List<string>(new Rules().getSubjList(specialty, year));
            string grFileDir = "Groups/" + name + "/" + name + ".txt";
            if (File.Exists(@grFileDir)) {
                StreamReader grDataReader = new StreamReader(File.Open(@grFileDir, FileMode.Open));
                grDataReader.ReadLine();
                grDataReader.ReadLine();
                string[] studenList = grDataReader.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
                students.AddRange(studenList);
                grDataReader.Close();
            }
        }

        public Group(string grName) {
            string[] grNameArr = grName.Split(".", StringSplitOptions.RemoveEmptyEntries);
            specialty = grNameArr[0];
            year = grNameArr[1];
            name = grName;
            students = new List<string>();
            subjects = new List<string>(new Rules().getSubjList(specialty, year));

            string grFileDir = "Groups/" + name + "/" + name + ".txt";
            if (File.Exists(@grFileDir)) {
                StreamReader grDataReader = new StreamReader(File.Open(@grFileDir, FileMode.Open));
                grDataReader.ReadLine();
                grDataReader.ReadLine();
                string[] studenList = grDataReader.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
                students.AddRange(studenList);
                grDataReader.Close();
            }
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