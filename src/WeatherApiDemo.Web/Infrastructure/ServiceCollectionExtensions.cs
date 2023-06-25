using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WeatherApiDemo.Web.Data;
using WeatherApiDemo.Web.Features.WeatherApi.Services;
using WeatherApiDemo.Web.Features.WeatherReadings.Services;
using WeatherApiDemo.Web.Infrastructure.Configuration;

namespace WeatherApiDemo.Web.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(
                opts =>
                {
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

        services.Configure<WeatherApiConfiguration>(configuration.GetSection("WeatherApi"));
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddDbContext<WeatherContext>(
            opts => opts.UseSqlite(configuration.GetConnectionString("WeatherDb"))
        );

        services.AddScoped<IWeatherApiService, WeatherApiService>();
        services.AddScoped<IWeatherReadingService, WeatherReadingService>();
        
        services.AddHttpClient<IWeatherApiService, WeatherApiService>(
            (svc, client) => client.BaseAddress = new Uri(svc.GetRequiredService<IOptions<WeatherApiConfiguration>>().Value.Url)
        );
    }
}