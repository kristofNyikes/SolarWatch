
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;
using SolarWatch.Services;

namespace SolarWatch;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

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
