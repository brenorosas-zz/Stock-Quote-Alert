using System;
namespace StockQuoteAlert{
    public class Asset{
        public int Id = 0;
        public string Ticker {get; set;}
        public decimal SaleReference {get; set;}
        public decimal PurchaseReference {get; set;}
        private static int _uniqueId = 0;
        public enum States{Sale, Purchase, Normal};
        public States State{get; set;}
        public Asset(string ticker, decimal saleReference, decimal purchaseReference, States state){
            Ticker = ticker;
            SaleReference = saleReference;
            PurchaseReference = purchaseReference;
            Id = _uniqueId;
            State = state;
            _uniqueId++;
        }
    }
}