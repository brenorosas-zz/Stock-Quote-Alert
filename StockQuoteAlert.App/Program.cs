using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
namespace StockQuoteAlert.App {
    class Program {
        private static async Task StartMonitor(List<Asset> assetList) {
            var tasks = new CommandLineTasks();
            var yahooIntegration = new YahooIntegration();
            string host = Environment.GetEnvironmentVariable("SMTP_HOST");
            int port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
            string emailAddress = Environment.GetEnvironmentVariable("EMAIL_ADDRESS");
            string emailPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
            string destinationEmail = Environment.GetEnvironmentVariable("DESTINATION_EMAIL");
            int maxConcurrentEmails = int.Parse(Environment.GetEnvironmentVariable("MAX_CONCURRENT_EMAILS"));
            var emailService = new EmailService(host, port, emailAddress, emailPassword);
            var monitor = new Monitor();
            while (true) {
                await monitor.ToMonitor(tasks, assetList, yahooIntegration, emailService, destinationEmail, maxConcurrentEmails);
                await Task.Delay(1000);
            }
        }
        private static async Task Worker(CommandLineTasks tasks, List<Asset> assetList) {
            while (true) {
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
        public static async Task Start(List<Asset> assetList, CommandLineTasks tasks) {
            var workers = new List<Task>();
            workers.Add(StartMonitor(assetList));
            workers.Add(Worker(tasks, assetList));
            await Task.WhenAll(workers);
        }
        static async Task Main(string[] args) {
            DotNetEnv.Env.Load("../.env");
            var tasks = new CommandLineTasks();
            var assetList = new List<Asset>();
            var workers = new List<Task>();
            for (int i = 0; i + 2 < args.Length; i += 3) {
                string[] aux = { "add", args[i], args[i + 1], args[i + 2] };
                tasks.Add(assetList, aux);
            }
            await Start(assetList, tasks);
        }
    }
}
