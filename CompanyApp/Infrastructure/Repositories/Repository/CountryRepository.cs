using CompanyApp.Domain.Models;
using CompanyApp.Infrastructure.Data;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApp.Infrastructure.Repositories.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(AppDbContext context, ILogger<CountryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all countries");
                var countries = await _context.Countries.ToListAsync();
                _logger.LogInformation("Successfully fetched all countries");
                return countries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all countries");
                throw new Exception("Error fetching countries", ex);
            }
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching country with ID: {Id}", id);
                var country = await _context.Countries.FindAsync(id);
                if (country == null)
                {
                    _logger.LogWarning("Country with ID {Id} not found", id);
                }
                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching country with ID: {Id}", id);
                throw new Exception($"Error fetching country with ID {id}", ex);
            }
        }

        public async Task AddAsync(Country country)
        {
            try
            {
                _logger.LogInformation("Adding new country: {CountryName}", country.CountryName);
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added country: {CountryName}", country.CountryName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new country: {CountryName}", country.CountryName);
                throw new Exception("Error adding country", ex);
            }
        }

        public async Task UpdateAsync(Country country)
        {
            try
            {
                _logger.LogInformation("Updating country with ID: {Id}", country.CountryId);
                _context.Countries.Update(country);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated country with ID: {Id}", country.CountryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating country with ID: {Id}", country.CountryId);
                throw new Exception($"Error updating country with ID {country.CountryId}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting country with ID: {Id}", id);
                var country = await _context.Countries.FindAsync(id);
                if (country != null)
                {
                    _context.Countries.Remove(country);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully deleted country with ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Country with ID {Id} not found", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting country with ID: {Id}", id);
                throw new Exception($"Error deleting country with ID {id}", ex);
            }
        }
    }
}
