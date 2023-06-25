using AutoFixture;
using FluentAssertions;
using WeatherApiDemo.Web.Data.Entities;
using WeatherApiDemo.Web.Features.WeatherReadings.Services;
using Xunit;

namespace WeatherApiDemo.IntegrationTests.Features.WeatherReadings.Services;

public class WeatherReadingServiceTests: BaseIntegrationTest<WeatherReadingService>
{
    public WeatherReadingServiceTests(TestServices testServices) : base(testServices)
    { }

    [Fact]
    public async Task Save_reading_saves_to_database()
    {
        var reading = Fixture.Build<WeatherReading>().Without(w => w.Id).Create();

        var savedReading = await Sut.SaveReading(reading);

        savedReading.Id.Should().NotBe(0);
        savedReading.ConditionText.Should().Be(reading.ConditionText);
        savedReading.TimeOfReading.Should().Be(reading.TimeOfReading);
        savedReading.TemperatureFahrenheit.Should().Be(reading.TemperatureFahrenheit);
    }
    
    [Fact]
    public async Task Created_reading_can_be_retrieved()
    {
        var reading = Fixture.Build<WeatherReading>().Without(w => w.Id).Create();
        WeatherContext.WeatherReadings.Add(reading);
        await WeatherContext.SaveChangesAsync();

        var retrievedReading = await Sut.GetReading(reading.Id);

        retrievedReading.Id.Should().NotBe(0);
        retrievedReading.ConditionText.Should().Be(reading.ConditionText);
        retrievedReading.TimeOfReading.Should().Be(reading.TimeOfReading);
        retrievedReading.TemperatureFahrenheit.Should().Be(reading.TemperatureFahrenheit);
    }
}