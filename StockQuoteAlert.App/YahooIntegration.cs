using System.Threading.Tasks;
using YahooFinanceApi;
namespace StockQuoteAlert.App{
    public class YahooIntegration{
        public async Task<System.Collections.Generic.IReadOnlyDictionary<string, YahooFinanceApi.Security> > YahooSymbol(string ticker) {
            var securities = await Yahoo.Symbols(ticker+".SA").QueryAsync();
            return securities;
        }
    }
}