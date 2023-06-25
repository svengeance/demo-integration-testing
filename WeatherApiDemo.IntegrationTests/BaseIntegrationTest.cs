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

    private DbConnection _dbConnection;

    public BaseIntegrationTest(TestServices testServices)
    {
        Fixture = new Fixture();
        ServiceScope = testServices.Services.CreateScope();
        WeatherContext = ServiceScope.ServiceProvider.GetRequiredService<WeatherContext>();
        
        _dbConnection = new SqliteConnection("DataSource=:memory:");
    }

    public async Task InitializeAsync()
    {
        await _dbConnection.OpenAsync();

        WeatherContext.Database.SetDbConnection(_dbConnection);

        await WeatherContext.Database.EnsureCreatedAsync();
        await WeatherContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbConnection.DisposeAsync();
     
        ServiceScope.Dispose();
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