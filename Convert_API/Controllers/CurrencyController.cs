using Convert_API.Services;
using Microsoft.AspNetCore.Mvc;
using Convert_API.Models;

namespace Convert_API.Controllers;

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
        return View(new ConvertViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(ConvertViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result =  await _currencyRateService.ConvertAsync(model.From, model.To, model.Amount);
        if(result == null)
        {
            ModelState.AddModelError(string.Empty, "Неверные данные или недоступен сервис курсов.");
            return View(model);
        }

        model.Result = Math.Round(result.Value, 2);
        return View(model);
    }
}