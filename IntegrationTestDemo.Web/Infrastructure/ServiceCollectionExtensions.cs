using System.Collections.Immutable;
using System.Text.Json;
using IntegrationTestDemo.Web.Data;
using IntegrationTestDemo.Web.Features.WeatherApi.Services;
using IntegrationTestDemo.Web.Features.WeatherReadings.Services;
using IntegrationTestDemo.Web.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IntegrationTestDemo.Web.Infrastructure;

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
            opts => opts.UseSqlite("DataSource=weatherdb.sqlite")
        );

        services.AddScoped<IWeatherApiService, WeatherApiService>();
        services.AddScoped<IWeatherReadingService, WeatherReadingService>();
        
        services.AddHttpClient<IWeatherApiService, WeatherApiService>(
            (svc, client) => client.BaseAddress = new Uri(svc.GetRequiredService<IOptions<WeatherApiConfiguration>>().Value.Url)
        );
    }
}