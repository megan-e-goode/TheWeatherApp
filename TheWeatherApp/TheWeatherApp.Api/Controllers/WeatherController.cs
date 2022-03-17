namespace TheWeatherApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheWeatherApp.Api.Models;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Uri _baseAddress;
    private readonly string _key;

    public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _baseAddress = new Uri(_configuration.GetValue<string>("ApiBaseUrl"));
        _key = _configuration.GetValue<string>("ApiKey");
    }

    [HttpGet(Name = "GetWeather")]
    public async Task<ActionResult<WeatherResponse>> Get(string cityName)
    {
        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = _baseAddress;
                var response = await client.GetAsync($"weather?q={cityName}&appid={_key}");
                response.EnsureSuccessStatusCode();

                var responseAsString = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseAsString);

                return Ok(new
                {
                    weatherResponse
                });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}