namespace WeatherApiDemo.Web.Data.Entities;

public record WeatherReading
{
    public int Id { get; init; }

    public string ConditionText { get; init; } = null!;
    
    public double TemperatureFahrenheit { get; init; }
    
    public long TimeOfReading { get; init; }
    
}