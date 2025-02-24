using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Domain.Models;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApp.Application.Services.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryService> _logger;

        public CountryService(ICountryRepository countryRepository, ILogger<CountryService> logger)
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            try
            {
                _logger.LogInformation("Fetching all countries");
                var countries = await _countryRepository.GetAllAsync();
                return countries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching countries");
                throw new Exception("Error fetching countries", ex);
            }
        }

        public async Task<Country> GetCountryById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching country with ID: {Id}", id);
                var country = await _countryRepository.GetByIdAsync(id);
                if (country == null)
                {
                    _logger.LogWarning("Country with ID: {Id} not found", id);
                }
                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching country with ID: {Id}", id);
                throw new Exception($"Error fetching country with ID {id}", ex);
            }
        }

        public async Task AddCountry(Country country)
        {
            try
            {
                _logger.LogInformation("Adding new country: {CountryName}.", country.CountryName);
                await _countryRepository.AddAsync(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new country: {CountryName}.", country.CountryName);
                throw new Exception($"Error adding country {country.CountryName}", ex);
            }
        }

        public async Task UpdateCountry(Country country)
        {
            try
            {
                _logger.LogInformation("Updating country with ID: {Id}", country.CountryId);
                await _countryRepository.UpdateAsync(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating country with ID: {Id}", country.CountryId);
                throw new Exception($"Error updating country with ID {country.CountryId}", ex);
            }
        }

        public async Task DeleteCountry(int id)
        {
            try
            {
                _logger.LogInformation("Deleting country with ID: {Id}", id);
                await _countryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting country with ID: {Id}", id);
                throw new Exception($"Error deleting country with ID {id}", ex);
            }
        }
    }
}
