namespace IntegrationTestDemo.Web.Features.WeatherApi.Models;

public record WeatherApiResult(
    Location Location,
    Current Current
);

public record Location(
    string Name,
    string Region,
    string Country,
    double Lat,
    double Lon,
    string Tz_Id,
    int Localtime_Epoch,
    string Localtime
);

public record Current(
    long Last_Updated_Epoch,
    string Last_Updated,
    double Temp_C,
    double Temp_F,
    int Is_Day,
    Condition Condition,
    double Wind_Mph,
    double Wind_Kph,
    int Wind_Degree,
    string Wind_Dir,
    double Pressure_Mb,
    double Pressure_In,
    double Precip_Mm,
    double Precip_In,
    int Humidity,
    int Cloud,
    double Feelslike_C,
    double Feelslike_F,
    double Vis_Km,
    double Vis_Miles,
    double Uv,
    double Gust_Mph,
    double Gust_Kph
);

public record Condition(
    string Text,
    string Icon,
    int Code
);

