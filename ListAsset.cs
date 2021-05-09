using System;
using System.Collections.Generic;
namespace StockQuoteAlert{
    public class ListAsset{
        public List<Asset> list_asset {get; set;}
        public ListAsset(){
            this.list_asset = new List<Asset> ();
        }
        public string Ls(){
            var report = new System.Text.StringBuilder();
            report.AppendLine("ID\tTICKER\tREFERÊNCIA PARA VENDA\tREFERÊNCIA PARA COMPRA");
            foreach(var asset in list_asset){
                report.AppendLine($"{asset.id}\t{asset.ticker}\tR$ {asset.sale_reference}\t\tR$ {asset.purchase_reference}");
            }
            return report.ToString();
        }
        public void Add(Asset asset){
            this.list_asset.Add(asset);
        }
        public void Remove(int id){
            int index = 0;
            bool exist = false;
            for(int i = 0; i < this.list_asset.Count; i++){
                if(this.list_asset[i].id == id){
                    index = i;
                    exist = true;
                    break;
                }
            }
            if(exist)
                this.list_asset.RemoveAt(index);
        }
    }
}