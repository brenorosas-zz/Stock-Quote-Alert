using System.Threading.Tasks;
using YahooFinanceApi;
using System;

namespace StockQuoteAlert.App {
    public class YahooIntegration {
        public virtual async Task<Tuple<string, decimal>> GetPrice(string assetTicker) {
            var securities = await Yahoo.Symbols(assetTicker + ".SA").QueryAsync();
            try {
                var x = securities[assetTicker + ".SA"];
            }
            catch {
                Console.WriteLine($"Ativo {assetTicker} não encontrado.");
                var error = new Tuple<string, decimal>("error", -1);
                return error;
            }
            var tickerAux = securities[assetTicker + ".SA"];
            var price = System.Convert.ToDecimal(tickerAux[Field.RegularMarketPrice]);
            var ok = new Tuple<string, decimal>("ok", price);
            return ok;
        }
    }
}