using System;
using System.Collections.Generic;

namespace Course_project {

    internal class Rules {

        public static string[,] S121 { get; } = new string[4, 2] {
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

        public static string[,] S122 { get; } = new string[4, 2] {
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

        public static string[,] S123 { get; } = new string[4, 2] {
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

        public static string[,] S172 { get; } = new string[4, 2] {
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

        public static List<string> getSubjList(string grName) {
            List<string> subjList = new List<string>();
            string[] grNameArr = grName.Split(".", StringSplitOptions.RemoveEmptyEntries);
            int spec = Convert.ToInt32(grNameArr[0]);
            int year = Convert.ToInt32(grNameArr[1]) - 1;
            int semester = DateTime.Now.Month > 6 ? 0 : 1; // 1st semester = 1
            switch (spec) {
                case 121:
                    subjList.AddRange(S121[year, semester].Split(","));
                    break;

                case 122:
                    subjList.AddRange(S122[year, semester].Split(","));
                    break;

                case 123:
                    subjList.AddRange(S123[year, semester].Split(","));
                    break;

                case 172:
                    subjList.AddRange(S172[year, semester].Split(","));
                    break;
            }
            return subjList;
        }
    }
}