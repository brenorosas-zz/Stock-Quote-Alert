using System;
using System.Collections.Generic;
namespace StockQuoteAlert{
    public class ListAsset{
        public List<Asset> AssetList {get; set;}
        public ListAsset(){
            this.AssetList = new List<Asset> ();
        }
        public string Ls(){
            var report = new System.Text.StringBuilder();
            report.AppendLine("ID\tTICKER\tREFERÊNCIA PARA VENDA\tREFERÊNCIA PARA COMPRA");
            foreach(var asset in AssetList){
                report.AppendLine($"{asset.Id}\t{asset.Ticker}\tR$ {asset.SaleReference}\t\tR$ {asset.PurchaseReference}");
            }
            return report.ToString();
        }
        public void Add(Asset asset){
            this.AssetList.Add(asset);
        }
        public void Remove(int id){
            int index = 0;
            bool exist = false;
            for(int i = 0; i < this.AssetList.Count; i++){
                if(this.AssetList[i].Id == id){
                    index = i;
                    exist = true;
                    break;
                }
            }
            if(exist)
                this.AssetList.RemoveAt(index);
        }
    }
}