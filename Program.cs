using System;
using System.Threading;
using System.Threading.Tasks;
using YahooFinanceApi;
using System.Collections.Generic;
using System.Net.Mail;
namespace StockQuoteAlert
{
    class Program
    {
        private static async Task Monitor(ListAsset list){
            while(true){
                var emails = new List<Task>();
                foreach (var asset in list.AssetList){
                    EmailService emailSender = new EmailService();
                    var securities = await Yahoo.Symbols(asset.Ticker+".SA").QueryAsync();
                    var ticker = securities[asset.Ticker+".SA"];
                    var price = System.Convert.ToDecimal(ticker[Field.RegularMarketPrice]);
                    if(price > asset.SaleReference){
                        //Funcionando, comentado para evitar spam e lembrar de tratar warning.
                        // Console.WriteLine("Enviando saporra");
                        emails.Add(emailSender.SendMail("brenorosas@hotmail.com", "ALERTA DE VENDA", $"O ativo {asset.Ticker} subiu acima do nível de referencia para venda de R${asset.SaleReference}, e está custando R${price}"));
                    }
                    if(price < asset.PurchaseReference){
                        //Funcionando, comentado para evitar spam e lembrar de tratar warning.
                        // Console.WriteLine("Enviando a outra porra");
                        emails.Add(emailSender.SendMail("brenorosas@hotmail.com", "ALERTA DE VENDA", $"O ativo {asset.Ticker} caiu abaixo do nível de referencia para venda de R${asset.PurchaseReference}, e está custando R${price}"));
                    }
                }
                // Console.WriteLine("Aguaradndo essa porra");
                await Task.WhenAll(emails);
                // Console.WriteLine("Aguardei esssa porra");
                await Task.Delay(1000);
            }
        }
        static void Main(string[] args)
        {
            var assets = new ListAsset();
            for(int i = 0; i + 2 < args.Length; i+=3){
                assets.Add(new Asset(args[i], decimal.Parse(args[i+1].Replace('.', ',')), decimal.Parse(args[i+2].Replace('.', ','))));
            }
            Task.Run(() => Monitor(assets));
            while(true){
                string[] input = Console.ReadLine().Split(' ');
                if(input[0] == "add")
                    assets.Add(new Asset(input[1], decimal.Parse(input[2].Replace('.', ',')), decimal.Parse(input[3].Replace('.', ','))));
                else if(input[0] == "ls")
                    Console.WriteLine(assets.Ls());
                else if(input[0] == "rm")
                    assets.Remove(int.Parse(input[1]));
                else
                    Console.WriteLine("unknow command");
            }
        }
    }
}
