namespace TheWeatherApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheWeatherApp.Api.Models;

// [ApiController]
// [Route("[controller]")]
[Route("api/[controller]")]
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

    // [HttpGet]
    [HttpGet("[action]")]
    public async Task<ActionResult<WeatherResponse>> GetWeather(string location = "London")
    {
        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = _baseAddress;
                var response = await client.GetAsync($"weather?q={location}&appid={_key}").ConfigureAwait(false);
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