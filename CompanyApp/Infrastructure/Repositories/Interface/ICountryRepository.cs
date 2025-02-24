﻿using CompanyApp.Domain.Models;

namespace CompanyApp.Infrastructure.Repositories.Interface
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country> GetByIdAsync(int id);
        Task AddAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
    }
}
