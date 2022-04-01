namespace TheWeatherApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheWeatherApp.Api.Models;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly Uri _baseAddress;
    private readonly string _key;

    public WeatherController(HttpClient httpClient, ILogger<WeatherController> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
        _baseAddress = new Uri(_configuration.GetValue<string>("ApiBaseUrl"));
        _key = _configuration.GetValue<string>("ApiKey");
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<WeatherResponse>> GetWeather(string location = "London")
    {
        _httpClient.BaseAddress = _baseAddress;
        var response = await _httpClient.GetAsync($"weather?q={location}&appid={_key}");

        if (response.StatusCode != HttpStatusCode.OK)
        {
            _logger.LogError($"Open Weather returned: {response.StatusCode} - {response.Content}");
            return BadRequest(response.Content);
        }

        var responseAsString = await response.Content.ReadAsStringAsync();
        var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseAsString);

        return Ok(new
        {
            weatherResponse
        });
    }
}