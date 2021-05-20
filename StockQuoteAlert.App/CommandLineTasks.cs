using System;
using System.Collections.Generic;
using System.Globalization;
namespace StockQuoteAlert.App
{
    public class CommandLineTasks
    {
        public CommandLineTasks() { }
        public string Add(List<Asset> assetList, string[] args)
        {
            if (args.Length < 4)
            {
                var error = "Informações incompletas";
                Console.WriteLine(error);
                return error;
            }
            var ticker = args[1];
            decimal saleReference = 0, purchaseReference = 0;
            try
            {
                saleReference = Convert.ToDecimal(args[2].Replace(',', '.'), new CultureInfo("en-US"));
            }
            catch
            {
                var error = "Digite o preço de referência para venda no formato: 22.67";
                Console.WriteLine(error);
                return error;
            }
            try
            {
                purchaseReference = Convert.ToDecimal(args[3].Replace(',', '.'), new CultureInfo("en-US"));
            }
            catch
            {
                var error = "Digite o preço de referência para compra no formato: 22.59";
                Console.WriteLine(error);
                return error;
            }
            if (saleReference < purchaseReference)
            {
                var error = "O valor referênica para venda deve ser maior que o de compra";
                Console.WriteLine(error);
                return error;
            }
            var asset = new Asset
            {
                Ticker = ticker,
                SaleReference = saleReference,
                PurchaseReference = purchaseReference,
                State = Asset.States.Normal
            };
            assetList.Add(asset);
            return "ok";
        }
        public string Remove(List<Asset> assetList, string[] args)
        {
            if (args.Length < 2)
            {
                var error = "Informações incompletas";
                Console.WriteLine(error);
                return error;
            }
            int id = -1;
            try
            {
                id = Convert.ToInt32(args[1]);
            }
            catch
            {
                var error = "Id no formato incorreto, favor digitar um número inteiro.";
                Console.WriteLine(error);
                return error;
            }
            int index = 0;
            var exist = false;
            for (int i = 0; i < assetList.Count; i++)
            {
                if (assetList[i].Id == id)
                {
                    index = i;
                    exist = true;
                    break;
                }
            }
            if (exist)
            {
                assetList.RemoveAt(index);
            }
            return "ok";
        }
        public string List(List<Asset> assetList)
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("ID\tTICKER\tREFERÊNCIA PARA VENDA\tREFERÊNCIA PARA COMPRA\t\tESTADO");
            foreach (var asset in assetList)
            {
                report.AppendLine($"{asset.Id}\t{asset.Ticker}\tR$ {asset.SaleReference}\t\tR$ {asset.PurchaseReference}\t\t\t{asset.State}");
            }
            return report.ToString();
        }
    }
}