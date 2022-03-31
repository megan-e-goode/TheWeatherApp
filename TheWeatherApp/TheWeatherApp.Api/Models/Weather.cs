namespace TheWeatherApp.Api.Models;
using Newtonsoft.Json;

public class Weather
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("main")]
    public string Type { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}
