using System.Collections.Generic;

namespace Course_project {
    public interface IGroup {
        string Name { get; set; }
        string Specialty { get; set; }
        List<string> Students { get; set; }
        List<string> Subjects { get; set; }
        string Year { get; set; }

        bool addStudent(string username);
        bool rmStudent(string username);
    }
}