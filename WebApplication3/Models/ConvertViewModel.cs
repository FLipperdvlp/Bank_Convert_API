namespace Bank_Convert_API.Models;
//Model for currency conversion
public class ConvertViewModel
{
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public decimal Amount { get; set; }
}
