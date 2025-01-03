﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarWatchApi.Models.City;
using SolarWatchApi.Models.SunriseSunset;

namespace SolarWatchApi.Data;

public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunset> SunriseSunsets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SunriseSunset>()
            .HasOne(s => s.City)
            .WithMany(c => c.SunriseSunsets)
            .HasForeignKey(s => s.CityId);
    }
}