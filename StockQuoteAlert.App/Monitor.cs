using System;
using System.Threading.Tasks;
using YahooFinanceApi;
using System.Collections.Generic;
namespace StockQuoteAlert.App {
    public class Monitor {
        public Monitor() { }
        public async Task ToMonitor(CommandLineTasks tasks, List<Asset> assetList, YahooIntegration yahooIntegration, EmailService emailService) {
            var emails = new List<Task>();
            var removeList = new List<int>();
            foreach (var asset in assetList) {
                var ok = await yahooIntegration.GetPrice(asset.Ticker);
                if (ok.Item1 == "error") {
                    removeList.Add(asset.Id);
                    continue;
                }
                var price = ok.Item2;
                if (price > asset.SaleReference && asset.State != Asset.States.Sale) {
                    asset.State = Asset.States.Sale;
                    emails.Add(emailService.SendMail(Environment.GetEnvironmentVariable("DESTINATION_EMAIL"), "ALERTA DE VENDA", $"O ativo {asset.Ticker} subiu acima do nível de referencia para venda de R${asset.SaleReference}, e está custando R${price}"));
                }
                else if (price < asset.PurchaseReference && asset.State != Asset.States.Purchase) {
                    asset.State = Asset.States.Purchase;
                    emails.Add(emailService.SendMail(Environment.GetEnvironmentVariable("DESTINATION_EMAIL"), "ALERTA DE VENDA", $"O ativo {asset.Ticker} caiu abaixo do nível de referencia para venda de R${asset.PurchaseReference}, e está custando R${price}"));
                }
                else if (price >= asset.PurchaseReference && price <= asset.SaleReference) {
                    asset.State = Asset.States.Normal;
                }
                if (emails.Count >= int.Parse(Environment.GetEnvironmentVariable("MAX_CONCURRENT_EMAILS"))) {
                    await Task.WhenAll(emails);
                }
            }
            foreach (int idToRemove in removeList) {
                var id = Convert.ToString(idToRemove);
                string[] aux = { "rm", id };
                tasks.Remove(assetList, aux);
            }
            await Task.WhenAll(emails);
        }
    }
}