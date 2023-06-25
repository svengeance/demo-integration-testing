using Microsoft.EntityFrameworkCore;
using WeatherApiDemo.Web.Data.Entities;

namespace WeatherApiDemo.Web.Data;

public class WeatherContext: DbContext
{
    public DbSet<WeatherReading> WeatherReadings { get; init; }

    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    { }
}