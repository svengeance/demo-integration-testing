using IntegrationTestDemo.Web.Data;
using IntegrationTestDemo.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;

// Begrudgingly use top-level minimal API setup magic
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

// Build the web application
var app = builder.Build();

// Configure the webapp pipeline
app.ConfigureWebApplication();

// Ensure our database is created and migrated appropriately
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WeatherContext>();
    
    await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
}

// Tada~
await app.RunAsync();