using CompanyApp.Domain.Models;

namespace CompanyApp.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company> GetCompanyById(int id);
        Task AddCompany(Company company);
        Task UpdateCompany(Company company);
        Task DeleteCompany(int id);
    }
}
