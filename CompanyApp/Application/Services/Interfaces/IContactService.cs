using CompanyApp.Domain.Models;

namespace CompanyApp.Application.Services.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<IEnumerable<Contact>> GetPagedContacts(int pageNumber);
        Task<Contact> GetContactById(int id);
        Task AddContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int id);
        Task<IEnumerable<Contact>> GetContactsWithDetailsAsync();
        Task<IEnumerable<Contact>> FilterContactsAsync(int? countryId, int? companyId);
    }
}
