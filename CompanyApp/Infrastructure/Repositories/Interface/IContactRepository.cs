using CompanyApp.Domain.Models;

namespace CompanyApp.Infrastructure.Repositories.Interface
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
        Task<IEnumerable<Contact>> GetContactsWithDetailsAsync();
        Task<IEnumerable<Contact>> FilterContactsAsync(int? countryId, int? companyId);

    }
}
