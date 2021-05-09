using System;
namespace StockQuoteAlert{
    public class Asset{
        public int id = 0;
        public string ticker {get; set;}
        public decimal lower_limit {get; set;}

        public decimal upper_limit {get; set;}
        private static int uniqueId = 0;

        public Asset() : this("XXXX", 0, 0) {}
        public Asset(string ticker, decimal lower_limit, decimal upper_limit){
            this.ticker = ticker;
            this.lower_limit = lower_limit;
            this.upper_limit = upper_limit;
            this.id = uniqueId;
            uniqueId++;
        }
    }
}