using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolarWatch;
using SolarWatch.Data;
using System.Net;

namespace SolarWatchTests;

public class SolarWatchWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(AppDbContext));

            if (dbContext != null)
            {
                services.Remove(dbContext);

            }

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(_dbName));

            using var scope = services.BuildServiceProvider().CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();

            SeedData(serviceProvider).GetAwaiter().GetResult();
            builder.UseEnvironment("Development");
        });
    }

    private async Task SeedData(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
        var authenticationSeeder = serviceProvider.GetService<AuthenticationSeeder>();
        authenticationSeeder!.AddRoles();

        var existingUsers = userManager.Users.ToList();
        foreach (var existingUser in existingUsers)
        {
            await userManager.DeleteAsync(existingUser);
        }

        var user = new IdentityUser
        {
            UserName = "testuser",
            Email = "testuser@user.com"
        };

        var password = "user123";
        var result = await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");

        if (!result.Succeeded)
        {
            throw new Exception("Failed to seed test user: " +
                                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}