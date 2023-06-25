using WeatherApiDemo.Web.Features.WeatherApi.Models;
using WeatherApiDemo.Web.Features.WeatherApi.Services;

namespace WeatherApiDemo.IntegrationTests.Stubs;

public class WeatherApiServiceStub: IWeatherApiService
{
    public Task<WeatherApiResult> RetrieveWeather(string query)
        => Task.FromResult(
            new WeatherApiResult(
                Location: new Location(
                    Name: "TestName",
                    Region: "TestRegion",
                    Country: "TestCountry",
                    Lat: 0.0,
                    Lon: 0.1,
                    Tz_Id: "TestTz_Id",
                    Localtime_Epoch: 0,
                    Localtime: "TestLocaltime"
                ),
                Current: new Current(
                    Last_Updated_Epoch: 0,
                    Last_Updated: "TestLast_Updated",
                    Temp_C: 0.2,
                    Temp_F: 0.3,
                    Is_Day: 1,
                    Condition: new Condition(
                        Text: "TestText",
                        Icon: "TestIcon",
                        Code: 5
                    ),
                    Wind_Mph: 0.4,
                    Wind_Kph: 0.5,
                    Wind_Degree: 2,
                    Wind_Dir: "TestWind_Dir",
                    Pressure_Mb: 0.6,
                    Pressure_In: 0.7,
                    Precip_Mm: 0.8,
                    Precip_In: 0.9,
                    Humidity: 3,
                    Cloud: 4,
                    Feelslike_C: 1.0,
                    Feelslike_F: 1.1,
                    Vis_Km: 1.2,
                    Vis_Miles: 1.3,
                    Uv: 1.4,
                    Gust_Mph: 1.5,
                    Gust_Kph: 1.6
                )
            )
        );
}