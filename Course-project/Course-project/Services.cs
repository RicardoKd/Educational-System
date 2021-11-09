using System;
using System.Collections.Generic;
using System.IO;

namespace Course_project {

    internal class Services {

        public static List<string> getOrder(string dir) {
            if (!dir.EndsWith("/"))
                dir += "/";

            StreamReader r = new StreamReader(File.Open(@dir + "order.txt", FileMode.Open));
            List<string> testOrder = new List<string>(r.ReadToEnd().Split(",", StringSplitOptions.RemoveEmptyEntries));
            r.Close();
            return testOrder;
        }
    }
}