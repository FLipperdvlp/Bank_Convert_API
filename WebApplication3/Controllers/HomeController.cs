using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly CurrencyRateService _currencyService;

    public HomeController(ILogger<HomeController> logger, CurrencyRateService currencyService)
    {
        _logger = logger;
        _currencyService = currencyService;
    }

    [HttpGet("user")]
    public ActionResult<object> GetUser()
    {
        var user = new
        {
            Name = "Gleb",
            Surname = "Renkas"
        };
        return Ok(user);
    }

    [HttpGet("currency")]
    public async Task<ActionResult<CurrencyInfo>> GetCurrency()
    {
        var currencyInfo = await _currencyService.GetCurrencyInfoAsync();
        if (!currencyInfo.From.Contains("UAH"))
            currencyInfo.From.Add("UAH");
        if (!currencyInfo.To.Contains("UAH"))
            currencyInfo.To.Add("UAH");
        return Ok(currencyInfo);
    }

    [HttpGet("currency/rates")]
    public async Task<ActionResult<List<CurrencyRate>>> GetCurrencyRates()
    {
        var rates = await _currencyService.GetCurrencyRatesAsync();
        if (rates == null)
        {
            return NotFound();
        }
        return Ok(rates);
    }
}