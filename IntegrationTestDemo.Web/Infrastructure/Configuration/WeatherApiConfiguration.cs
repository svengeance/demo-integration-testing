namespace IntegrationTestDemo.Web.Infrastructure.Configuration;

public record WeatherApiConfiguration
{
    public string ConnectionString { get; init; } = null!;
    public string Url { get; init; } = null!;
}