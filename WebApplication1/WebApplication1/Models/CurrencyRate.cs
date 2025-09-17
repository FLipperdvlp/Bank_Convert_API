using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Bank_Convert_API.Models;

public class CurrencyRate
{
    [JsonProperty("ccy")]
    public string Ccy { get; set; }
    
    [JsonProperty("base_ccy")]
    public string BaseCcy { get; set; }
    
    [JsonProperty("buy")]
    public  string Buy { get; set; }
    
    [JsonProperty("sale")]
    public string Sale { get; set; }
}