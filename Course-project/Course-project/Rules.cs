using System;

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

        public static string[] getSubjList(string spec, string year) {
            int semester = Services.GetCurrentSemester() - 1;
            int yearConv = Convert.ToInt32(year) - 1;
            return Convert.ToInt32(spec) switch {
                121 => S121[yearConv, semester].Split(", "),
                122 => S122[yearConv, semester].Split(", "),
                123 => S123[yearConv, semester].Split(", "),
                172 => S172[yearConv, semester].Split(", "),
                _ => null,
            };
        }

        public static string[] getSubjList(string grName) {
            string[] grNameArr = grName.Split(".", StringSplitOptions.RemoveEmptyEntries);
            int year = Convert.ToInt32(grNameArr[1]) - 1;
            int semester = Services.GetCurrentSemester() - 1;
            return Convert.ToInt32(grNameArr[0]) switch {
                121 => S121[year, semester].Split(", "),
                122 => S122[year, semester].Split(", "),
                123 => S123[year, semester].Split(", "),
                172 => S172[year, semester].Split(", "),
                _ => null,
            };
        }
    }
}