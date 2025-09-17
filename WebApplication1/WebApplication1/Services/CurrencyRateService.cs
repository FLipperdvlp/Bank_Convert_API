using System.Net.Http.Json;
using Bank_Convert_API.Models;

namespace Bank_Convert_API.Services;

public class CurrencyRateService
{
    private readonly HttpClient _httpClient;

    public CurrencyRateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<List<CurrencyRate>> GetCurrencyRatesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CurrencyRate>>(
            "https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5");
    }

    public async Task<decimal?> ConvertAsync(string from, string to, decimal amount)
    {
        var rates = await GetCurrencyRatesAsync();
        if (rates == null || !rates.Any()) return null;

        from = from.ToUpper();
        to = to.ToUpper();

        decimal amountInUah;

        if (from == "UAH") amountInUah = amount;
        else
        {
            var fromRate = rates.FirstOrDefault(r => r.Ccy == from);
            if (fromRate == null || !decimal.TryParse(fromRate.Sale, out var saleRate))
                return null;

            amountInUah = amount * saleRate; 
        }

        if (to == "UAH") return amountInUah;
        else
        {
            var toRate = rates.FirstOrDefault(r => r.Ccy == to);
            if (toRate == null || !decimal.TryParse(toRate.Buy, out var buyRate))
                return null;

            return amountInUah / buyRate;  
        }
    }
}