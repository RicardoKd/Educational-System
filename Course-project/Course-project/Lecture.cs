using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    public class Lecture {
        private string name;
        private string text;
        private int semester;
        private List<string> imgList = new List<string>();

        public Lecture(string name, string text, int semester, List<string> imgList) {
            this.name = name;
            this.text = text;
            this.imgList = imgList;
            this.semester = semester;
        }

        public Lecture() {
            name = null;
            text = null;
            semester = 1;
        }

        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }
        public int Semester { get => semester; set => semester = value; }
        public List<string> ImgList { get => imgList; set => imgList = value; }

        public bool addImg(string fileName) {
            if (!string.IsNullOrEmpty(fileName)) {
                ImgList.Add(fileName);
                return true;
            }
            return false;
        }

        public void WriteToJson(string dir) {
            Directory.CreateDirectory(@dir);
            StreamWriter sw = new StreamWriter(File.Open(@dir + name + ".json", FileMode.Create));
            string output = JsonConvert.SerializeObject(this);
            sw.WriteLine(output);
            sw.Close();
        }
    }
}