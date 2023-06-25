using IntegrationTestDemo.Web.Data.Entities;
using IntegrationTestDemo.Web.Features.WeatherReadings.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestDemo.Web.Features.WeatherReadings;

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