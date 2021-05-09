using System;
namespace StockQuoteAlert{
    public class Asset{
        public int id = 0;
        public string ticker {get; set;}
        public decimal sale_reference {get; set;}

        public decimal purchase_reference {get; set;}
        private static int uniqueId = 0;

        public Asset() : this("XXXX", 0, 0) {}
        public Asset(string ticker, decimal sale_reference, decimal purchase_reference){
            this.ticker = ticker;
            this.sale_reference = sale_reference;
            this.purchase_reference = purchase_reference;
            this.id = uniqueId;
            uniqueId++;
        }
    }
}