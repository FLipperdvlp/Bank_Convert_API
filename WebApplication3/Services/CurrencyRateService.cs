using WebApplication3.Models;

namespace WebApplication3.Services;

public class CurrencyRateService
{
    private readonly HttpClient _httpClient;

    public CurrencyRateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<List<CurrencyRate>?> GetRatesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<CurrencyRate>>(
                "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5"
            );
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<CurrencyInfo> GetCurrencyInfoAsync()
    {
        var rates = await GetRatesAsync();
        var currencyInfo = new CurrencyInfo();

        if (rates != null)
        {
            foreach (var rate in rates)
            {
                currencyInfo.From.Add(rate.Ccy);
                currencyInfo.To.Add(rate.Ccy);
            }
        }
        return currencyInfo;
    }

    public async Task<List<CurrencyRate>?> GetCurrencyRatesAsync()
    {
        return await GetRatesAsync();
    }
}