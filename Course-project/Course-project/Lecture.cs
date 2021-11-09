using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    public class Lecture : ILecture {

        public Lecture(string name, string text, int semester, List<string> imgList) {
            Name = name;
            Text = text;
            Semester = semester;
            ImgList = new List<string>(imgList);
        }

        public Lecture() {
            Name = null;
            Text = null;
            Semester = 1;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public int Semester { get; set; }
        public List<string> ImgList { get; set; }

        public bool addImg(string fileName) {
            if (!string.IsNullOrEmpty(fileName)) {
                ImgList.Add(fileName);
                return true;
            }
            return false;
        }

        public void WriteToJson(string dir) {
            Directory.CreateDirectory(@dir);
            StreamWriter sw = new StreamWriter(File.Open(@dir + Name + ".json", FileMode.Create));
            string output = JsonConvert.SerializeObject(this);
            sw.WriteLine(output);
            sw.Close();
        }
    }
}