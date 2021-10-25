using System.Collections.Generic;

namespace Lab_5 {

    internal class Organization {
        protected string name;
        protected int yearlyIncome;
        protected int turnover;
        protected int workers;
        private List<Factory> clients = new List<Factory>();

        public string Name { get => name; set => name = value; }
        public int YearlyIncome { get => yearlyIncome; set => yearlyIncome = value; }
        public int Workers { get => workers; set => workers = value; }
        public int Turnover { get => turnover; set => turnover = value; }
        public List<Factory> Clients { get => clients; }

        public void addClient(Factory factory) {
            clients.Add(factory);
        }
    }
}