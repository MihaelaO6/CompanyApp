using CompanyApp.Domain.Models;
using CompanyApp.Infrastructure.Data;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Infrastructure.Repositories.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(AppDbContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all contacts");
                var contacts = await _context.Contacts
                    .Include(c => c.Company)
                    .Include(c => c.Country)
                    .ToListAsync();
                _logger.LogInformation("Successfully fetched all contacts");
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching");
                throw new Exception("Error fetching contacts", ex);
            }
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching contact with ID: {Id}", id);
                var contact = await _context.Contacts.FindAsync(id);
                if (contact == null)
                {
                    _logger.LogWarning("Contact with ID {Id} not found", id);
                }
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contact with ID: {Id}", id);
                throw new Exception($"Error fetching contact with ID {id}", ex);
            }
        }

        public async Task AddAsync(Contact contact)
        {
            try
            {
                _logger.LogInformation("Adding new contact: {ContactName}", contact.ContactName);
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added contact: {ContactName}", contact.ContactName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new contact: {ContactName}", contact.ContactName);
                throw new Exception("Error adding contact", ex);
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            try
            {
                _logger.LogInformation("Updating contact with ID: {Id}", contact.ContactId);
                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated contact with ID: {Id}", contact.ContactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact with ID: {Id}", contact.ContactId);
                throw new Exception($"Error updating contact with ID {contact.ContactId}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting contact with ID: {Id}", id);
                var contact = await _context.Contacts.FindAsync(id);
                if (contact != null)
                {
                    _context.Contacts.Remove(contact);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully deleted contact with ID: {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Contact with ID {Id} not found", id);
                }
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
                var contacts = await _context.Contacts
                    .Include(c => c.Company)
                    .Include(c => c.Country)
                    .ToListAsync();
                _logger.LogInformation("Successfully fetched contacts with details");
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching");
                throw new Exception("Error fetching contacts with details", ex);
            }
        }

        public async Task<IEnumerable<Contact>> FilterContactsAsync(int? countryId, int? companyId)
        {
            try
            {
                _logger.LogInformation("Filtering contacts started");

                var query = _context.Contacts.AsQueryable();

                if (countryId.HasValue)
                {
                    query = query.Where(c => c.CountryId == countryId.Value);
                }
                if (companyId.HasValue)
                {
                    query = query.Where(c => c.CompanyId == companyId.Value);
                }

                var contacts = await query
                    .Include(c => c.Company)
                    .Include(c => c.Country)
                    .ToListAsync();

                _logger.LogInformation("Successfully filtered contacts");
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while filtering contacts");
                throw new Exception("Error filtering contacts", ex);
            }
        }
    }
}
