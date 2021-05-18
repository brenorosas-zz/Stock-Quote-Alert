using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using YahooFinanceApi;
namespace StockQuoteAlert{
    public class CommandLineTasks{
        public CommandLineTasks(){}
        public void Add(List<Asset> assetList, string[] args){
            if(args.Length < 3){
                Console.WriteLine("Informações incompletas");
                return;
            }
            string ticker = args[1];
            // var price = System.Convert.ToDecimal(breno[Field.RegularMarketPrice]);
            // Console.WriteLine(price);
            decimal SaleReference = 0, PurchaseReference = 0;
            try{
                SaleReference = Convert.ToDecimal(args[2].Replace(',', '.'), new CultureInfo("en-US"));
            }
            catch{
                Console.WriteLine("Digite o preço de referência para venda no formato: 22.67");
                return;
            }
            try{
                PurchaseReference = Convert.ToDecimal(args[3].Replace(',', '.'), new CultureInfo("en-US"));
            }
            catch{
                Console.WriteLine("Digite o preço de referência para compra no formato: 22.59");
                return;
            }
            Asset asset = new Asset(ticker, SaleReference, PurchaseReference, Asset.States.Normal);
            assetList.Add(asset);
        }
        public void Remove(List<Asset> assetList, string[] args){
            if(args.Length < 2){
                Console.WriteLine("Informações incompletas");
                return;
            }
            int id = -1;
            try{
                id = Convert.ToInt32(args[1]);
            }
            catch{
                Console.WriteLine("Id no formato incorreto, favor digitar um número inteiro.");
            }
            int index = 0;
            bool exist = false;
            for(int i = 0; i < assetList.Count; i++){
                if(assetList[i].Id == id){
                    index = i;
                    exist = true;
                    break;
                }
            }
            if(exist){
                assetList.RemoveAt(index);
            }
        }
        public void List(List<Asset> assetList){
            var report = new System.Text.StringBuilder();
            report.AppendLine("ID\tTICKER\tREFERÊNCIA PARA VENDA\tREFERÊNCIA PARA COMPRA\t\tESTADO");
            foreach(var asset in assetList){
                report.AppendLine($"{asset.Id}\t{asset.Ticker}\tR$ {asset.SaleReference}\t\tR$ {asset.PurchaseReference}\t\t\t{asset.State}");
            }
            Console.WriteLine(report.ToString());
        }
    }
}