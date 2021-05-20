using Xunit;
using System.Collections.Generic;
using StockQuoteAlert.App;
using FluentAssertions;
namespace StockQuoteAlert.Tests
{
    public class CommandLineTests
    {
        [Fact]
        public void TestAdd()
        {
            var command = new CommandLineTasks();
            var assetList = new List<Asset>();
            var assetListTest = new List<Asset>();
            string[] args;
            args = new string[] { "add", "PETR4", "22.67", "testError" };
            var error = command.Add(assetListTest, args);
            error.Should().Be("Digite o preço de referência para compra no formato: 22.59");
            args = new string[] { "add", "PETR4", "testError", "22.50"};
            error = command.Add(assetListTest, args);
            error.Should().Be("Digite o preço de referência para venda no formato: 22.67");
            args = new string[] { "add", "PETR4", "22.59", "22.67" };
            error = command.Add(assetListTest, args);
            error.Should().Be("O valor referênica para venda deve ser maior que o de compra");
            args = new string[] { "add", "PETR4", "22.67", "22.59"};
            error = command.Add(assetListTest, args);
            error.Should().Be("ok");
            var asset = new Asset
            {
                Ticker = "PETR4",
                SaleReference = (decimal)22.67,
                PurchaseReference = (decimal)22.59,
                State = Asset.States.Normal
            };
            assetList.Add(asset);
            assetListTest.Count.Should().Be(assetList.Count);
            assetListTest[0].Ticker.Should().BeEquivalentTo(assetListTest[0].Ticker);
            assetListTest[0].SaleReference.Should().Be(assetListTest[0].SaleReference);
            assetListTest[0].PurchaseReference.Should().Be(assetListTest[0].PurchaseReference);
            assetListTest[0].State.Should().BeEquivalentTo(assetListTest[0].State);
            args = new string[] { "add", "PETR4", "22.67"};
            error = command.Add(assetListTest, args);
            error.Should().Be("Informações incompletas");
        }
        [Fact]
        public void TestRemove()
        {
            string[] args;
            var command = new CommandLineTasks();
            var assetList = new List<Asset>();
            var assetListTest = new List<Asset>();
            var asset = new Asset
            {
                Ticker = "PETR4",
                SaleReference = (decimal)22.67,
                PurchaseReference = (decimal)22.59,
                State = Asset.States.Normal
            };
            assetListTest.Add(asset);
            args = new string[] { "rm", "9999" };
            command.Remove(assetListTest, args);
            assetListTest.Should().NotBeEquivalentTo(assetList);
            args = new string[] { "rm", "testError" };
            var error = command.Remove(assetListTest, args);
            error.Should().Be("Id no formato incorreto, favor digitar um número inteiro.");
            args = new string[] { "rm", $"{asset.Id}" };
            command.Remove(assetListTest, args);
            assetListTest.Should().BeEquivalentTo(assetList);
            args = new string[] { "rm"};
            error = command.Remove(assetListTest, args);
            error.Should().Be("Informações incompletas");
        }
        [Fact]
        public void TestList()
        {
            var command = new CommandLineTasks();
            var assetList = new List<Asset>();
            var report = new System.Text.StringBuilder();
            report.AppendLine("ID\tTICKER\tREFERÊNCIA PARA VENDA\tREFERÊNCIA PARA COMPRA\t\tESTADO");
            var actual = command.List(assetList);
            var expected = report.ToString();
            actual.Should().BeEquivalentTo(expected);
            var assetTest = new Asset
            {
                Ticker = "PETR4",
                SaleReference = (decimal)22.67,
                PurchaseReference = (decimal)22.59,
                State = Asset.States.Normal
            };
            assetList.Add(assetTest);
            foreach (var asset in assetList)
            {
                report.AppendLine($"{asset.Id}\t{asset.Ticker}\tR$ {asset.SaleReference}\t\tR$ {asset.PurchaseReference}\t\t\t{asset.State}");
            }
            expected = report.ToString();
            actual = command.List(assetList);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}