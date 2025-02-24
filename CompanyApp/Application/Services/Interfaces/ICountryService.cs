using CompanyApp.Domain.Models;

namespace CompanyApp.Application.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountryById(int id);
        Task AddCountry(Country country);
        Task UpdateCountry(Country country);
        Task DeleteCountry(int id);
    }
}
