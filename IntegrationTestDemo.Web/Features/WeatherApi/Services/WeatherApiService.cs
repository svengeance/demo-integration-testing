using IntegrationTestDemo.Web.Data.Entities;
using IntegrationTestDemo.Web.Features.WeatherApi.Models;
using IntegrationTestDemo.Web.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace IntegrationTestDemo.Web.Features.WeatherApi.Services;

public interface IWeatherApiService
{
    Task<WeatherApiResult> RetrieveWeather(string query);
}

public class WeatherApiService : IWeatherApiService
{
    private readonly ILogger<WeatherApiService> _logger;
    private readonly HttpClient _httpClient;
    private readonly WeatherApiConfiguration _weatherApiOptions;

    public WeatherApiService(ILogger<WeatherApiService> logger, HttpClient httpClient, IOptions<WeatherApiConfiguration> weatherApiOptions)
    {
        _logger = logger;
        _httpClient = httpClient;
        _weatherApiOptions = weatherApiOptions.Value;
    }

    public async Task<WeatherApiResult> RetrieveWeather(string query)
    {
        var queryStringParameters = new Dictionary<string, string?>
        {
            ["key"] = _weatherApiOptions.ConnectionString,
            ["q"] = query,
            ["aqi"] = "no"
        };

        var uri = GetWeatherApiUri(queryStringParameters);
        var apiResult = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
        apiResult.EnsureSuccessStatusCode();

        _logger.LogInformation("Retrieved weather reading for {Query}", query);
        
        return await apiResult.Content.ReadFromJsonAsync<WeatherApiResult>()
               ?? throw new InvalidOperationException("Weather API response could not be read");
    }
    
    private Uri GetWeatherApiUri(IEnumerable<KeyValuePair<string, string?>> queryParameters)
    {
        if (_httpClient.BaseAddress is null)
            throw new InvalidOperationException("HTTP Client was not correctly configured, BaseAddress is null");

        return new UriBuilder
        {
            Host = _httpClient.BaseAddress.Host,
            Fragment = _httpClient.BaseAddress.Fragment,
            Path = _httpClient.BaseAddress.AbsolutePath,
            Query = QueryString.Create(queryParameters).Value,
            Scheme = _httpClient.BaseAddress.Scheme
        }.Uri;
    }
}