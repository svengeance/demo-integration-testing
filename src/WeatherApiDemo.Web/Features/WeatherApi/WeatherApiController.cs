using Microsoft.AspNetCore.Mvc;
using WeatherApiDemo.Web.Data.Entities;
using WeatherApiDemo.Web.Features.WeatherApi.Extensions;
using WeatherApiDemo.Web.Features.WeatherApi.Services;
using WeatherApiDemo.Web.Features.WeatherReadings.Services;

namespace WeatherApiDemo.Web.Features.WeatherApi;

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