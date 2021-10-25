using System;

namespace Lab_5 {

    internal class Factory : Organization, ITurnover {

        public event EventHandler AddFactory;

        int ITurnover.Turnover() {
            Turnover = (int)(YearlyIncome + Clients.Count * 0.3 + Workers * 500);
            if (AddFactory != null)
                AddFactory(this, null);
            return Turnover;
        }
    }
}