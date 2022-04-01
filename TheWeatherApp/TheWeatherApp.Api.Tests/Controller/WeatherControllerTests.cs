namespace TheWeatherApp.Api.Tests;

using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheWeatherApp.Api.Controllers;
using TheWeatherApp.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

public class WeatherControllerTests
{
    private WeatherController _weatherController;
    private Mock<ILogger<WeatherController>> _loggerMock;    
    private IConfiguration _configuration;
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<WeatherController>>();        

        var inMemorySettings = new Dictionary<string, string> {
            {"ApiBaseUrl", "https://api.openweathermap.org/"},
            {"ApiKey", "abcdefg"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        SetupHttpClient();

        _weatherController = new WeatherController(_httpClient, _loggerMock.Object, _configuration);
    }

    [Test]
    public async Task GetWeather_Returns_OK_Response_With_Populated_Data()
    {
        var okObjectResultData = new
        {
            weatherResponse = GetWeatherResponse()
        };

        SetupHttpClient(HttpStatusCode.OK, GetWeatherResponseAsString());

        var expectedData = new OkObjectResult(okObjectResultData);

        var response = await _weatherController.GetWeather();
        var responseData = response.Result as OkObjectResult;

        responseData.Should().BeEquivalentTo(expectedData);
    }

    [Test]
    public async Task GetWeather_Returns_BadRequest_Response_When_HttpClient_Call_Fails()
    {
        var responseMessage = "Bad Request retrieving weather";
        var expectedData = new BadRequestObjectResult(responseMessage);

        var response = await _weatherController.GetWeather();
        var responseData = response.Result as OkObjectResult;

        responseData.Should().BeEquivalentTo(expectedData);
    }

    private WeatherResponse GetWeatherResponse()
    {
        return new WeatherResponse
        {
            CityName = "London",
            Temperature = new Temperature
            {
                Actual = 273.76,
                FeelsLike = 273.76,
                Min = 39,
                Max = 389,
                Pressure = 320,
                Humidity = 3908
            },
            Weather = new List<Weather>
            {
                new Weather
                {
                    Id = 1,
                    Description = "Scattered clouds",
                    Type = "Clouds"
                }
            }
        };
    }

    private string GetWeatherResponseAsString()
    {
        return JsonConvert.SerializeObject(GetWeatherResponse());
    }

    private void SetupHttpClient(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string content = "Bad Request retrieving weather")
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = statusCode,
            Content = new StringContent(content),
        };

        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response).Verifiable();

        _httpClient = new HttpClient(handlerMock.Object);
    }
}