using FluentValidation;
using ProgiCodingChallenge.API.Handlers.Vehicle.CalculatePrice;
using ProgiCodingChallenge.Core.Vehicles;
using Serilog;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IVehicleFactory, VehicleFactory>();
builder.Services.AddScoped<IVehicleCalculatePriceCommandHandler, VehicleCalculatePriceCommandHandler>();
builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddValidatorsFromAssemblyContaining<VehicleCalculatePriceCommandValidator>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Lê do appsettings.json
    .CreateLogger();
builder.Services.AddSerilog();
builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.MapControllers();
app.UseCors("AllowAllOrigins");
app.UseSerilogRequestLogging();

app.Run();

namespace ProgiCodingChallenge.API
{
    [ExcludeFromCodeCoverage]
    public sealed partial class Program
    {
        private Program() { }
    }
}