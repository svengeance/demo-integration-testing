using IntegrationTestDemo.Web.Data.Entities;
using IntegrationTestDemo.Web.Features.WeatherApi.Models;

namespace IntegrationTestDemo.Web.Features.WeatherApi.Extensions;

public static class WeatherApiResultExtensions
{
    public static WeatherReading MapToWeatherReading(this WeatherApiResult weatherApiResult)
        => new()
        {
            ConditionText = weatherApiResult.Current.Condition.Text,
            TemperatureFahrenheit = weatherApiResult.Current.Temp_F,
            TimeOfReading = weatherApiResult.Current.Last_Updated_Epoch
        };
}