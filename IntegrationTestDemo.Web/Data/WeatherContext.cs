using IntegrationTestDemo.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTestDemo.Web.Data;

public class WeatherContext: DbContext
{
    public DbSet<WeatherReading> WeatherReadings { get; init; }

    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    { }
}