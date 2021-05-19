using System;
using System.Threading.Tasks;
using YahooFinanceApi;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
namespace StockQuoteAlert
{
    class Program
    {
        private static async Task Monitor(List<Asset> assetList){
            var tasks = new CommandLineTasks();
            while(true){
                var emails = new List<Task>();
                List<int> removeList = new List<int>();
                foreach (var asset in assetList){
                    EmailService emailSender = new EmailService();
                    var securities = await Yahoo.Symbols(asset.Ticker+".SA").QueryAsync();
                    try{
                        var x = securities[asset.Ticker+".SA"];
                    }
                    catch{
                        Console.WriteLine($"Ativo {asset.Ticker} não encontrado.");
                        removeList.Add(asset.Id);
                        continue;
                    }
                    var ticker = securities[asset.Ticker+".SA"];
                    var price = System.Convert.ToDecimal(ticker[Field.RegularMarketPrice]);
                    if(price > asset.SaleReference && asset.State != Asset.States.Sale){
                        asset.State = Asset.States.Sale;
                        emails.Add(emailSender.SendMail(Environment.GetEnvironmentVariable("DESTINATION_EMAIL"), "ALERTA DE VENDA", $"O ativo {asset.Ticker} subiu acima do nível de referencia para venda de R${asset.SaleReference}, e está custando R${price}"));
                    }
                    else if(price < asset.PurchaseReference && asset.State != Asset.States.Purchase){
                        asset.State = Asset.States.Purchase;
                        emails.Add(emailSender.SendMail(Environment.GetEnvironmentVariable("DESTINATION_EMAIL"), "ALERTA DE VENDA", $"O ativo {asset.Ticker} caiu abaixo do nível de referencia para venda de R${asset.PurchaseReference}, e está custando R${price}"));
                    }
                    else if(price >= asset.PurchaseReference && price <= asset.SaleReference){
                        asset.State = Asset.States.Normal;
                    }
                    if(emails.Count >= 5){
                        await Task.WhenAll(emails);
                    }
                }
                foreach(int idToRemove in removeList){
                    string id = Convert.ToString(idToRemove);
                    string[] aux = {"rm", id};
                    tasks.Remove(assetList, aux);
                }
                await Task.WhenAll(emails);
                await Task.Delay(1000);
            }
        }
        private static async Task Worker(CommandLineTasks tasks, List<Asset> assetList){
            while(true){
                string[] commands = Console.ReadLine().Split(' ');
                var rootCommand = new RootCommand("Command Service");
                var commandAdd = new Command("add");
                var commandList = new Command("list");
                commandList.AddAlias("ls");
                var commandRemove = new Command("remove");
                commandRemove.AddAlias("rm");
                commandAdd.Description = "Adiciona um ativo ao monitoramento, ex: add PETR4 22.67 22.59";
                commandRemove.Description = "Remove um ativo tomando como referência o ID. rm <id>";
                commandList.Description = "Lista os ativos em monitoramento";
                commandAdd.Handler = CommandHandler.Create(() => tasks.Add(assetList, commands));
                commandList.Handler = CommandHandler.Create(() => Console.WriteLine(tasks.List(assetList)));
                commandRemove.Handler = CommandHandler.Create(() => tasks.Remove(assetList, commands));
                rootCommand.Add(commandAdd);
                rootCommand.Add(commandList);
                rootCommand.Add(commandRemove);
                await rootCommand.InvokeAsync(commands[0]);
            }
        }
        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load(".env");
            var tasks = new CommandLineTasks();
            var assetList = new List<Asset>();
            var workers = new List<Task>();
            for(int i = 0; i + 2 < args.Length; i+=3){
                string[] aux = {"add", args[i], args[i+1], args[i+2]};
                tasks.Add(assetList, aux);
            }
            workers.Add(Monitor(assetList));
            workers.Add(Worker(tasks, assetList));
            await Task.WhenAll(workers);
        }
    }
}
