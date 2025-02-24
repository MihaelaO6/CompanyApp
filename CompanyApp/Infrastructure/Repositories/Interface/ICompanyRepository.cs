using CompanyApp.Domain.Models;

namespace CompanyApp.Infrastructure.Repositories.Interface
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(int id);

    }
}
