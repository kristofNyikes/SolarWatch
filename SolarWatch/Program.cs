
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SolarWatch.Data;
using SolarWatch.Services;
using SolarWatch.Services.Authentication;
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
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true; // Optional for better readability
            });

        builder.Services.AddScoped<IJsonProcessor, JsonProcessor>();
        builder.Services.AddScoped<ICurrentWeatherDataProvider, CurrentWeatherDataProvider>();
        builder.Services.AddScoped<ISunriseSunsetDataProvider, SunriseSunsetDataProvider>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddDbContext<SunriseSunsetWeatherApiContext>(options =>
        {
            options.UseSqlServer(
                "Server = localhost,1433; Database = WeatherApi; User Id = sa; Password = strongSolarWatchPassword123; Encrypt = false; ");
        });

        builder.Services.AddDbContext<UsersContext>(options =>
        {
            options.UseSqlServer(
                "Server = localhost,1433; Database = WeatherApi; User Id = sa; Password = strongSolarWatchPassword123; Encrypt = false; ");
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("JwtSettings")["ValidIssuer"],
                    ValidAudience = builder.Configuration.GetSection("JwtSettings")["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:IssuerSigningKey"]!))
                };
            });

        builder.Services
            .AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<UsersContext>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<ISunriseSunsetRepository, SunriseSunsetRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<AuthenticationSeeder>();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
        authenticationSeeder.AddRoles();
        authenticationSeeder.AddAdmin();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //app.UseCors("AllowReactApp");

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
