using Xunit;
using System.Collections.Generic;
using StockQuoteAlert.App;
using System.Collections.ObjectModel;
using FluentAssertions;
using System;
using NSubstitute;
using System.Threading.Tasks;
namespace StockQuoteAlert.Tests {
    public class MonitorTests {
        [Fact]
        public async Task TestToMonitor() {
            Console.WriteLine("Debug");
            var yahooIntegration = Substitute.For<YahooIntegration>();
            string host = "hostTest";
            int port = 32;
            string emailAddress = "emailAddressTest";
            string emailPassword = "emailPasswordTest";
            var emailService = Substitute.For<EmailService>(host, port, emailAddress, emailPassword);
            var result1 = new Tuple<string, decimal>("ok", 100);
            yahooIntegration.GetPrice(Arg.Any<string>()).Returns(result1);
            var tasks = new CommandLineTasks();
            var assetList = new List<Asset>();
            var asset1 = new Asset {
                Ticker = "PETR4",
                SaleReference = decimal.MaxValue - 1,
                PurchaseReference = decimal.MaxValue,
                State = Asset.States.Normal
            };
            var asset2 = new Asset {
                Ticker = "ABEV3",
                SaleReference = decimal.MaxValue - 1,
                PurchaseReference = decimal.MaxValue,
                State = Asset.States.Normal
            };
            var asset3 = new Asset {
                Ticker = "B3SA3",
                SaleReference = decimal.MaxValue - 1,
                PurchaseReference = decimal.MaxValue,
                State = Asset.States.Normal
            };
            var monitor = new Monitor();
            assetList.Add(asset1);
            assetList.Add(asset2);
            assetList.Add(asset3);
            await monitor.ToMonitor(tasks, assetList, yahooIntegration, emailService, "testmail@test.com", 2);
            foreach (var asset in assetList) {
                asset.State.Should().BeEquivalentTo(Asset.States.Purchase);
                asset.State = Asset.States.Normal;
                asset.SaleReference = 0;
                asset.PurchaseReference = 1;
            }
            await monitor.ToMonitor(tasks, assetList, yahooIntegration, emailService, "testmail@test.com", 2);
            foreach (var asset in assetList) {
                asset.State.Should().BeEquivalentTo(Asset.States.Sale);
            }
        }
    }
}