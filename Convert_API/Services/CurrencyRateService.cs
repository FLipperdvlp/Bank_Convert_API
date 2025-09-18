using System.Globalization;
using System.Net.Http;
using Convert_API.Models;
using Newtonsoft.Json;

namespace Convert_API.Services;

public class CurrencyRateService
{
        private readonly HttpClient _httpClient;

        public CurrencyRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<List<CurrencyRate>?> GetCurrencyRatesAsync()
        {
            var json = await _httpClient.GetStringAsync("https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5");
            return JsonConvert.DeserializeObject<List<CurrencyRate>>(json);
        }

        public async Task<decimal?> ConvertAsync(string from, string to, decimal amount)
        {
            var rates = await GetCurrencyRatesAsync();
            if (rates == null || !rates.Any()) return null;

            from = from.ToUpperInvariant();
            to = to.ToUpperInvariant();

            decimal amountInUah;

            if (from == "UAH")
            {
                amountInUah = amount;
            }
            else
            {
                var fromRate = rates.FirstOrDefault(r => r.Ccy == from);
                if (fromRate == null || !decimal.TryParse(fromRate.Sale, NumberStyles.Number, CultureInfo.InvariantCulture, out var saleRate))
                {
                    return null;
                }

                amountInUah = amount * saleRate;
            }

            if (to == "UAH") return amountInUah;

            var toRate = rates.FirstOrDefault(r => r.Ccy == to);
            if (toRate == null || !decimal.TryParse(toRate.Buy, NumberStyles.Number, CultureInfo.InvariantCulture, out var buyRate))
            {
                return null;
            }

            return amountInUah / buyRate;
        }
}