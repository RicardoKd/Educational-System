using System;

namespace Course_project {

    internal class Rules {

        private readonly string[,] s121 = new string[4, 2] {
            {
                "English, Math, C++, Git, Systematic View, Physics, History",
                "English, Math, C#, OOP, Algorithms, Physics, Philosophy"
            },
            {
                "English, Math, C#, OOP, Design, Testing",
                "English, Math, C#, OOP, Databases, Docker"
            },
            {
                "English, Math, JS, Networks, Databases, Soft Skills",
                "English, Math, JS, Networks, PM, Soft Skills"
            },
            {
                "English, Math, Python, Project Architecture, Soft Skills",
                "English, Math, Python, Project Architecture, Soft Skills"
            }
        };

        private readonly string[,] s122 = new string[4, 2] {
            {
                "English, Math, C++, Git, Systematic View, Physics, History",
                "English, Math, C#, OOP, Algorithms, Physics, Philosophy"
            },
            {
                "English, Math, C#, OOP, Design, Testing",
                "English, Math, C#, OOP, Databases, Docker"
            },
            {
                "English, Math, JS, Networks, Databases, Soft Skills",
                "English, Math, JS, Networks, PM, Soft Skills"
            },
            {
                "English, Math, Python, Project Architecture, Soft Skills",
                "English, Math, Python, Project Architecture, Soft Skills"
            }
        };

        private readonly string[,] s123 = new string[4, 2] {
            {
                "English, Math, C++, Git, Systematic View, Physics, History",
                "English, Math, C#, OOP, Algorithms, Physics, Philosophy"
            },
            {
                "English, Math, C#, OOP, Design, Testing",
                "English, Math, C#, OOP, Databases, Docker"
            },
            {
                "English, Math, JS, Networks, Databases, Soft Skills",
                "English, Math, JS, Networks, PM, Soft Skills"
            },
            {
                "English, Math, Python, Project Architecture, Soft Skills",
                "English, Math, Python, Project Architecture, Soft Skills"
            }
        };

        private string[,] s172 = new string[4, 2] {
            {
                "English, Math, C++, Git, Systematic View, Physics, History",
                "English, Math, C#, OOP, Algorithms, Physics, Philosophy"
            },
            {
                "English, Math, C#, OOP, Design, Testing",
                "English, Math, C#, OOP, Databases, Docker"
            },
            {
                "English, Math, JS, Networks, Databases, Soft Skills",
                "English, Math, JS, Networks, PM, Soft Skills"
            },
            {
                "English, Math, Python, Project Architecture, Soft Skills",
                "English, Math, Python, Project Architecture, Soft Skills"
            }
        };

        public Rules() {
        }

        public string[,] S121 { get => s121; }
        public string[,] S122 { get => s122; }
        public string[,] S123 { get => s123; }
        public string[,] S172 { get => s172; }

        public string[] getSubjList(string spec, string year) {
            int semester = DateTime.Now.Month > 6 ? 0 : 1; // 1st semester = 1
            switch (Convert.ToInt32(spec)) {
                case 121:
                    return S121[Convert.ToInt32(year) - 1, semester].Split(",");

                case 122:
                    return S122[Convert.ToInt32(year) - 1, semester].Split(",");

                case 123:
                    return S123[Convert.ToInt32(year) - 1, semester].Split(",");

                case 172:
                    return S172[Convert.ToInt32(year) - 1, semester].Split(",");

                default:
                    return null;
            }
        }

        public string getSubjList1(string spec, string year) {
            int semester = DateTime.Now.Month > 6 ? 0 : 1; // 1st semester = 1
            switch (Convert.ToInt32(spec)) {
                case 121:
                    return S121[Convert.ToInt32(year) - 1, semester];

                case 122:
                    return S122[Convert.ToInt32(year) - 1, semester];

                case 123:
                    return S123[Convert.ToInt32(year) - 1, semester];

                case 172:
                    return S172[Convert.ToInt32(year) - 1, semester];

                default:
                    return null;
            }
        }
    }
}