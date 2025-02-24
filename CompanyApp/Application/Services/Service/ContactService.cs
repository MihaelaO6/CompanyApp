using CompanyApp.Domain.Models;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Infrastructure.Pagination;

namespace CompanyApp.Application.Services.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly PaginationSettings _paginationSettings;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactRepository contactRepository,
                              IOptions<PaginationSettings> paginationSettings,
                              ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository;
            _paginationSettings = paginationSettings.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetPagedContacts(int pageNumber)
        {
            try
            {
                _logger.LogInformation("Fetching contacts for page {PageNumber}", pageNumber);
                var contacts = await _contactRepository.GetAllAsync();
                var pagedContacts = contacts.Skip((pageNumber - 1) * _paginationSettings.PageSize)
                                            .Take(_paginationSettings.PageSize)
                                            .ToList();
                return pagedContacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contacts for page {PageNumber}", pageNumber);
                throw new Exception("Error fetching paged contacts", ex);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            try
            {
                _logger.LogInformation("Fetching all contacts");
                var contacts = await _contactRepository.GetAllAsync();
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all contacts");
                throw new Exception("Error fetching all contacts", ex);
            }
        }

        public async Task<Contact> GetContactById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching contact with ID: {Id}", id);
                var contact = await _contactRepository.GetByIdAsync(id);
                if (contact == null)
                {
                    _logger.LogWarning("Contact with ID: {Id} not found", id);
                }
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contact with ID: {Id}", id);
                throw new Exception($"Error fetching contact with ID {id}", ex);
            }
        }

        public async Task AddContact(Contact contact)
        {
            try
            {
                _logger.LogInformation("Adding new contact with name: {ContactName}.", contact.ContactName);
                await _contactRepository.AddAsync(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new contact with name: {ContactName}.", contact.ContactName);
                throw new Exception($"Error adding contact {contact.ContactName}", ex);
            }
        }

        public async Task UpdateContact(Contact contact)
        {
            try
            {
                _logger.LogInformation("Updating contact with ID: {Id}", contact.ContactId);
                await _contactRepository.UpdateAsync(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact with ID: {Id}", contact.ContactId);
                throw new Exception($"Error updating contact with ID {contact.ContactId}", ex);
            }
        }

        public async Task DeleteContact(int id)
        {
            try
            {
                _logger.LogInformation("Deleting contact with ID: {Id}", id);
                await _contactRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting contact with ID: {Id}", id);
                throw new Exception($"Error deleting contact with ID {id}", ex);
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsWithDetailsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching contacts with details");
                var contacts = await _contactRepository.GetContactsWithDetailsAsync();
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contacts with details");
                throw new Exception("Error fetching contacts with details", ex);
            }
        }

        public async Task<IEnumerable<Contact>> FilterContactsAsync(int? countryId, int? companyId)
        {
            try
            {
                _logger.LogInformation("Filtering contacts by CountryId: {CountryId} and CompanyId: {CompanyId}", countryId, companyId);
                var contacts = await _contactRepository.GetAllAsync();
                var filteredContacts = contacts
                    .Where(c => (!countryId.HasValue || c.CountryId == countryId) &&
                                (!companyId.HasValue || c.CompanyId == companyId))
                    .ToList();
                _logger.LogInformation("Successfully filtered contact");
                return filteredContacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while filtering contacts by CountryId: {CountryId} and CompanyId: {CompanyId}", countryId, companyId);
                throw new Exception($"Error filtering contacts with CountryId: {countryId} and CompanyId: {companyId}", ex);
            }
        }
    }
}
