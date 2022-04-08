namespace TheWeatherApp.Api.Models;
using Newtonsoft.Json;

public class WeatherResponse
{
    [JsonProperty("name")]
    public string CityName { get; set; }

    [JsonProperty("weather")]
    public List<Weather> Weather { get; set; }

    [JsonProperty("main")]
    public Temperature Temperature { get; set; }
}
