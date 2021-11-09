using System;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    public class Group : IGroup {

        public Group(string specialty, string year) {
            Specialty = specialty;
            Year = year;
            Name = specialty + "." + year;
            Students = new List<string>();
            Subjects = new List<string>(new Rules().getSubjList(specialty, year));
            string grFileDir = "Groups/" + Name + "/" + Name + ".txt";
            if (File.Exists(@grFileDir)) {
                StreamReader grDataReader = new StreamReader(File.Open(@grFileDir, FileMode.Open));
                grDataReader.ReadLine();
                grDataReader.ReadLine();
                string[] studenList = grDataReader.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
                Students.AddRange(studenList);
                grDataReader.Close();
            }
        }

        public Group(string grName) {
            string[] grNameArr = grName.Split(".", StringSplitOptions.RemoveEmptyEntries);
            Specialty = grNameArr[0];
            Year = grNameArr[1];
            Name = grName;
            Students = new List<string>();
            Subjects = new List<string>(new Rules().getSubjList(Specialty, Year));

            string grFileDir = "Groups/" + Name + "/" + Name + ".txt";
            if (File.Exists(@grFileDir)) {
                StreamReader grDataReader = new StreamReader(File.Open(@grFileDir, FileMode.Open));
                grDataReader.ReadLine();
                grDataReader.ReadLine();
                string[] studenList = grDataReader.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries);
                Students.AddRange(studenList);
                grDataReader.Close();
            }
        }

        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Year { get; set; }
        public List<string> Students { get; set; }
        public List<string> Subjects { get; set; }

        public bool addStudent(string username) {
            if (!Students.Contains(username)) {
                Students.Add(username);
                return true;
            }
            return false;
        }

        public bool rmStudent(string username) {
            if (Students.Contains(username)) {
                Students.Remove(username);
                return true;
            }
            return false;
        }
    }
}