namespace TheWeatherApp.Api.Models;
using System.Text.Json.Serialization;

public class Temperature
{
    [JsonPropertyName("temp")]
    public decimal Actual { get; set; }

    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; }

    [JsonPropertyName("temp_min")]
    public decimal Min { get; set; }

    [JsonPropertyName("temp_max")]
    public decimal Max { get; set; }

    [JsonPropertyName("pressure")]
    public decimal Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public decimal Humidity { get; set; }
}