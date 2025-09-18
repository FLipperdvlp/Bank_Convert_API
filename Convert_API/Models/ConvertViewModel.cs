using System.ComponentModel.DataAnnotations;

namespace Convert_API.Models;

public class ConvertViewModel
{
    [Required]
    [Display(Name = "Из валюты")]
    public string From { get; set; } = "USD";

    [Required]
    [Display(Name = "В валюту")]
    public string To { get; set; } = "UAH";

    [Range(0.01, double.MaxValue, ErrorMessage = "Сумма должна быть больше 0")]
    [Display(Name = "Сумма")]
    public decimal Amount { get; set; } = 1m;

    [Display(Name = "Результат")]
    public decimal? Result { get; set; }
}


