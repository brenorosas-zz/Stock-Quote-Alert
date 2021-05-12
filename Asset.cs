using System;
namespace StockQuoteAlert{
    public class Asset{
        public int Id = 0;
        public string Ticker {get; set;}
        public decimal SaleReference {get; set;}

        public decimal PurchaseReference {get; set;}
        private static int _uniqueId = 0;

        public Asset() : this("XXXX", 0, 0) {}
        public Asset(string Ticker, decimal SaleReference, decimal PurchaseReference){
            this.Ticker = Ticker;
            this.SaleReference = SaleReference;
            this.PurchaseReference = PurchaseReference;
            this.Id = _uniqueId;
            _uniqueId++;
        }
    }
}