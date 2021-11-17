using System.Collections.Generic;

namespace Course_project {

    public interface ILecture {
        List<string> ImgList { get; set; }
        string Name { get; set; }
        int Semester { get; set; }
        string Text { get; set; }
        List<TestMark> StudentMarksList { get; set; }

        bool addImg(string fileName);

        void WriteToJson(string dir);
    }
}