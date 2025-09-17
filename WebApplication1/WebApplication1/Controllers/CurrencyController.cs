using Bank_Convert_API.Models;
using Bank_Convert_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAppl.Controllers;

public class CurrencyController : Controller
{
    private readonly CurrencyRateService  _currencyRateService;

    public CurrencyController(CurrencyRateService currencyRateService)
    {
        _currencyRateService = currencyRateService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost("convert")]
    public async Task<IActionResult> Index(string from, string to, decimal amount)
    {
        var result =  await _currencyRateService.ConvertAsync(from, to, amount);
        
        if(result == null) return BadRequest(new {Error = "Invalid"});

        return Ok(new
        {
            From = from.ToUpper(),
            To = to.ToUpper(),
            Amount = amount.ToString(),
            Result = Math.Round(result.Value, 2)
        });
    }
}