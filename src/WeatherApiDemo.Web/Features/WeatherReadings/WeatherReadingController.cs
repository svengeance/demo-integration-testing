using Microsoft.AspNetCore.Mvc;
using WeatherApiDemo.Web.Data.Entities;
using WeatherApiDemo.Web.Features.WeatherReadings.Services;

namespace WeatherApiDemo.Web.Features.WeatherReadings;

[ApiController]
[Route("[controller]")]
public class WeatherReadingController: ControllerBase
{
    private readonly IWeatherReadingService _weatherReadingService;

    public WeatherReadingController(IWeatherReadingService weatherReadingService)
        => _weatherReadingService = weatherReadingService;

    [HttpGet("{id}")]
    public async Task<WeatherReading> Get(int id)
        => await _weatherReadingService.GetReading(id);
}