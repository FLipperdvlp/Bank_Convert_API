namespace Bank_Convert_API.Models;
//Model for currency information
public class CurrencyInfo
{
    public List<string> From { get; set; } = new List<string>();
    public List<string> To { get; set; } = new List<string>();
}