using Xunit;
using System;
using System.Collections.Generic;

namespace StockQuoteAlert.Tests{
    public class CommandLineTests{
        [Fact]
        public void AddTest(){
            var command = new CommandLineTasks();
            List<Asset> assetList = new List<Asset>();
            List<Asset> assetListTest = new List<Asset>();
            string[] args;
            args = new string[] {"add", "PETR4", "22.67", "testError"};
            command.Add(assetListTest, args);
            Assert.Equal(assetList, assetListTest);
            args = new string[] {"add", "PETR4", "testError", "22.50"};
            command.Add(assetListTest, args);
            Assert.Equal(assetList, assetListTest);
            args = new string[] {"add", "PETR4", "22.67", "22.59"};
            command.Add(assetListTest, args);
            var asset = new Asset("PETR4", (decimal)22.67, (decimal)22.59);
            assetList.Add(asset);
            Assert.Equal(assetList.Count, assetListTest.Count);
            Assert.Equal(assetList[0].Ticker, assetListTest[0].Ticker);
            Assert.Equal(assetList[0].SaleReference, assetListTest[0].SaleReference);
            Assert.Equal(assetList[0].PurchaseReference, assetListTest[0].PurchaseReference);
            Assert.Equal(assetList[0].State, assetListTest[0].State);
        }
        [Fact]
        public void RemoveTest(){
            string[] args;
            var command = new CommandLineTasks();
            List<Asset> assetList = new List<Asset>();
            List<Asset> assetListTest = new List<Asset>();
            var asset = new Asset("PETR4", (decimal)22.67, (decimal)22.59);
            assetListTest.Add(asset);
            args = new string[] {"rm", "9999"};
            command.Remove(assetListTest, args);
            Assert.NotEqual(assetListTest, assetList);
            args = new string[] {"rm", "testError"};
            command.Remove(assetListTest, args);
            Assert.NotEqual(assetListTest, assetList);
            args = new string[] {"rm", $"{asset.Id}"};
            command.Remove(assetListTest, args);
            Assert.Equal(assetListTest, assetList);
        }
        [Fact]
        public void ListTest(){
            var command = new CommandLineTasks();
            List<Asset> assetList = new List<Asset>();
            var report = new System.Text.StringBuilder();
            report.AppendLine("ID\tTICKER\tREFERÊNCIA PARA VENDA\tREFERÊNCIA PARA COMPRA\t\tESTADO");
            string actual = command.List(assetList);
            string expected = report.ToString();
            Assert.Equal(actual, expected);
            var asset = new Asset("PETR4", (decimal)22.67, (decimal)22.59);
            assetList.Add(asset);
            foreach(var x in assetList){
                report.AppendLine($"{x.Id}\t{x.Ticker}\tR$ {x.SaleReference}\t\tR$ {x.PurchaseReference}\t\t\t{x.State}");
            }
            expected = report.ToString();
            actual = command.List(assetList);
            Assert.Equal(actual, expected);
        }
    }
}
