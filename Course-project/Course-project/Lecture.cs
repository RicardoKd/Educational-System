using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    internal class Lecture {
        private string name;
        private int index;
        private string text;
        private List<string> imgList = new List<string>();

        public Lecture(string name, string text, int index, List<string> imgList) {
            this.name = name;
            this.index = index;
            this.text = text;
            this.imgList = imgList;
        }

        public Lecture() {
            name = null;
            index = 0;
            text = null;
        }

        public string Name { get => name; set => name = value; }
        public int Index { get => index; set => index = value; }
        public string Text { get => text; set => text = value; }
        public List<string> ImgList { get => imgList; set => imgList = value; }

        public bool addImg(string fileName) {
            if (!string.IsNullOrEmpty(fileName)) {
                ImgList.Add(fileName);
                return true;
            }
            return false;
        }

        public void WriteToJson(string pathToFile) {
            Directory.CreateDirectory(@pathToFile);
            StreamWriter sw = new StreamWriter(File.Open(@pathToFile + index + ".json", FileMode.Create));
            string output = JsonConvert.SerializeObject(this);
            sw.WriteLine(output);
            sw.Close();
        }
    }
}