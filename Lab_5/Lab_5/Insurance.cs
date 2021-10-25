using System;

namespace Lab_5 {

    internal class Insurance : Organization, ITurnover {
        private static bool f = false;

        public static bool F { get => f; set => f = value; }

        int ITurnover.Turnover() {
            if (Clients.Count > 0)
                Turnover = (int)(YearlyIncome + Clients.Count * 40000 + Workers * 1000);
            else
                Turnover = 0;
            return Turnover;
        }

        public void DoEvent(object sender, EventArgs e) {
            Turnover = (int)(YearlyIncome + Clients.Count * 40000 + Workers * 10000);
        }
    }
}