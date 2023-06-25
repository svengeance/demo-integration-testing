using WeatherApiDemo.Web.Data.Entities;
using WeatherApiDemo.Web.Features.WeatherApi.Models;

namespace WeatherApiDemo.Web.Features.WeatherApi.Extensions;

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