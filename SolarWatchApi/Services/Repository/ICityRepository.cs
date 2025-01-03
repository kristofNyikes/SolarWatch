﻿using SolarWatchApi.Models.City;

namespace SolarWatchApi.Services.Repository;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<IEnumerable<City>> GetAllWithSunriseSunsetAsync();
    Task<City?> GetByNameAsync(string name);
    Task AddAsync(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(City city);
}