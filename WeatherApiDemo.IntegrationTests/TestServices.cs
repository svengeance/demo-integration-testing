using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WeatherApiDemo.IntegrationTests.Stubs;
using WeatherApiDemo.Web.Features.WeatherApi.Services;
using WeatherApiDemo.Web.Infrastructure;
using Xunit;

namespace WeatherApiDemo.IntegrationTests;

public class TestServices: IAsyncLifetime
{
    public IServiceProvider Services => _webApplication.Services;
    
    private readonly WebApplication _webApplication;
    
    public TestServices()
    {
        var webApplicationBuilder = WebApplication.CreateBuilder();

        ConfigureTestConfiguration(webApplicationBuilder.Configuration);

        webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration);

        ConfigureTestServices(webApplicationBuilder.Services);
        
        _webApplication = webApplicationBuilder.Build();
    }

    private static void ConfigureTestServices(IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Scoped<IWeatherApiService, WeatherApiServiceStub>());
    }

    private static void ConfigureTestConfiguration(IConfigurationBuilder builder)
        => builder.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["ConnectionStrings:WeatherDb"] = "DataSource=:memory:"
        });

    public Task InitializeAsync()
        => Task.CompletedTask;

    public async Task DisposeAsync()
        => await _webApplication.DisposeAsync();
}