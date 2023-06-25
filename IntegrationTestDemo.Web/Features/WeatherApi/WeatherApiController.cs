using IntegrationTestDemo.Web.Data.Entities;
using IntegrationTestDemo.Web.Features.WeatherApi.Extensions;
using IntegrationTestDemo.Web.Features.WeatherApi.Services;
using IntegrationTestDemo.Web.Features.WeatherReadings.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestDemo.Web.Features.WeatherApi;

[ApiController]
[Route("[controller]")]

public class WeatherApiController: ControllerBase
{
    private readonly IWeatherApiService _weatherApiService;
    private readonly IWeatherReadingService _weatherReadingService;

    public WeatherApiController(IWeatherApiService weatherApiService, IWeatherReadingService weatherReadingService)
    {
        _weatherApiService = weatherApiService;
        _weatherReadingService = weatherReadingService;
    }

    [HttpPost("Log")]
    public async Task<WeatherReading> Log([FromQuery] string query)
    {
        var weatherApiResult = await _weatherApiService.RetrieveWeather(query);

        var weatherReading = weatherApiResult.MapToWeatherReading();
        var savedReading = await _weatherReadingService.SaveReading(weatherReading);

        return savedReading;
    }
}