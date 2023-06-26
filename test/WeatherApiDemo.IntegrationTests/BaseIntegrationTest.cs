using System.Data.Common;
using AutoFixture;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherApiDemo.Web.Data;
using Xunit;

namespace WeatherApiDemo.IntegrationTests;

public abstract class BaseIntegrationTest: IAsyncLifetime, IClassFixture<TestServices>
{
    protected IServiceScope ServiceScope { get; }
    protected WeatherContext WeatherContext { get; }
    protected Fixture Fixture { get; }
    
    public BaseIntegrationTest(TestServices testServices)
    {
        Fixture = new Fixture();
        ServiceScope = testServices.Services.CreateScope();
        WeatherContext = ServiceScope.ServiceProvider.GetRequiredService<WeatherContext>();
    }

    public async Task InitializeAsync()
    {
        await WeatherContext.Database.EnsureDeletedAsync();
        await WeatherContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        ServiceScope.Dispose();
        return Task.CompletedTask;
    }
}

public abstract class BaseIntegrationTest<TSut> : BaseIntegrationTest where TSut : class
{
    protected TSut Sut { get; }

    protected BaseIntegrationTest(TestServices testServices) : base(testServices)
    {
        var serviceInterface = typeof(TSut).GetInterfaces().Single();
        Sut = (TSut)ServiceScope.ServiceProvider.GetRequiredService(serviceInterface);
    }
}