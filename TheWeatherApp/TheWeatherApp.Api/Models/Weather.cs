namespace TheWeatherApp.Api.Models;
using System.Text.Json.Serialization;

public class Weather
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("main")]
    public string Type { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}
