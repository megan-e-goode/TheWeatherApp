namespace TheWeatherApp.Api.Models;
using System.Text.Json.Serialization;

public class WeatherResponse
{
    [JsonPropertyName("name")]
    public string CityName { get; set; }

    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }

    [JsonPropertyName("main")]
    public Temperature Temperature { get; set; }
}
