
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;
using SolarWatch.Services;
using SolarWatch.Services.Repository;

namespace SolarWatch;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true; // Optional for better readability
            });

        builder.Services.AddScoped<IJsonProcessor, JsonProcessor>();
        builder.Services.AddScoped<ICurrentWeatherDataProvider, CurrentWeatherDataProvider>();
        builder.Services.AddScoped<ISunriseSunsetDataProvider, SunriseSunsetDataProvider>();
        builder.Services.AddDbContext<SunriseSunsetWeatherApiContext>(options =>
        {
            options.UseSqlServer(
                "Server = localhost,1433; Database = WeatherApi; User Id = sa; Password = strongSolarWatchPassword123; Encrypt = false; ");
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<ISunriseSunsetRepository, SunriseSunsetRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
