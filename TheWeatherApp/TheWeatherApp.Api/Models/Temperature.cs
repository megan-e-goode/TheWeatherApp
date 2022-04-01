namespace TheWeatherApp.Api.Models;
using Newtonsoft.Json;

public class Temperature
{
    [JsonProperty("temp")]
    public double Actual { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public double Min { get; set; }

    [JsonProperty("temp_max")]
    public double Max { get; set; }

    [JsonProperty("pressure")]
    public double Pressure { get; set; }

    [JsonProperty("humidity")]
    public double Humidity { get; set; }
}