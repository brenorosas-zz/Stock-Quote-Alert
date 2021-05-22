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
            Environment.SetEnvironmentVariable("EMAIL_ADDRESS", "testmail@test.com");
            Environment.SetEnvironmentVariable("EMAIL_PASSWORD", "testPassword");
            Environment.SetEnvironmentVariable("DESTINATION_EMAIL", "testDestinationMail@test.com");
            Environment.SetEnvironmentVariable("SMTP_HOST", "smtp.testmail.com");
            Environment.SetEnvironmentVariable("SMTP_PORT", "587");
            Environment.SetEnvironmentVariable("MAX_CONCURRENT_EMAILS", "2");
            var yahooIntegration = Substitute.For<YahooIntegration>();
            var emailService = Substitute.For<EmailService>();
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
            await monitor.ToMonitor(tasks, assetList, yahooIntegration, emailService);
            foreach (var asset in assetList) {
                asset.State.Should().BeEquivalentTo(Asset.States.Purchase);
                asset.State = Asset.States.Normal;
                asset.SaleReference = 0;
                asset.PurchaseReference = 1;
            }
            await monitor.ToMonitor(tasks, assetList, yahooIntegration, emailService);
            foreach (var asset in assetList) {
                asset.State.Should().BeEquivalentTo(Asset.States.Sale);
            }
        }
    }
}