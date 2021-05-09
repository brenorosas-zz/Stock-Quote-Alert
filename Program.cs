using System;
using System.Threading;
using System.Threading.Tasks;
using YahooFinanceApi;
namespace StockQuoteAlert
{
    class Program
    {
        private static async Task Monitor(ListAsset list){
            EmailController EmailSender = new EmailController();
            while(true){
                foreach (var asset in list.list_asset){
                    var securities = await Yahoo.Symbols(asset.ticker+".SA").QueryAsync();
                    var ticker = securities[asset.ticker+".SA"];
                    var price = System.Convert.ToDecimal(ticker[Field.RegularMarketPrice]);
                    if(price > asset.sale_reference){
                        //Funcionando, comentado para evitar spam e lembrar de tratar warning.
                        // Task.Run(() => EmailSender.SendMail("brenorosas@hotmail.com", "ALERTA DE VENDA", $"O ativo {asset.ticker} subiu acima do nível de referencia para venda de R${asset.sale_reference}, e está custando R${price}"));
                    }
                    if(price < asset.purchase_reference){
                        //Funcionando, comentado para evitar spam e lembrar de tratar warning.
                        // Task.Run(() => EmailSender.SendMail("brenorosas@hotmail.com", "ALERTA DE VENDA", $"O ativo {asset.ticker} caiu abaixo do nível de referencia para venda de R${asset.purchase_reference}, e está custando R${price}"));
                    }
                }
                Thread.Sleep(1000);
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
