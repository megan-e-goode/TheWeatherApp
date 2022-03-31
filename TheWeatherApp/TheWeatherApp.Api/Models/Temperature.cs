namespace TheWeatherApp.Api.Models;
using Newtonsoft.Json;

public class Temperature
{
    [JsonProperty("temp")]
    public decimal Actual { get; set; }

    [JsonProperty("feels_like")]
    public decimal FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public decimal Min { get; set; }

    [JsonProperty("temp_max")]
    public decimal Max { get; set; }

    [JsonProperty("pressure")]
    public decimal Pressure { get; set; }

    [JsonProperty("humidity")]
    public decimal Humidity { get; set; }
}