﻿using IntegrationTestDemo.Web.Data;
using IntegrationTestDemo.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTestDemo.Web.Features.WeatherReadings.Services;

public interface IWeatherReadingService
{
    Task<WeatherReading> SaveReading(WeatherReading reading);
    Task<WeatherReading> GetReading(int id);
}

public class WeatherReadingService : IWeatherReadingService
{
    private readonly WeatherContext _weatherContext;

    public WeatherReadingService(WeatherContext weatherContext)
        => _weatherContext = weatherContext;

    public async Task<WeatherReading> SaveReading(WeatherReading reading)
    {
        _weatherContext.Add(reading);
        
        await _weatherContext.SaveChangesAsync();

        return reading;
    }

    public async Task<WeatherReading> GetReading(int id)
        => await _weatherContext.WeatherReadings.AsNoTracking().FirstAsync(f => f.Id == id);
}