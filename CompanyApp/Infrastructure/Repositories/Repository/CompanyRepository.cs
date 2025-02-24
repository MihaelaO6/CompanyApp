using CompanyApp.Domain.Models;
using CompanyApp.Infrastructure.Data;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApp.Infrastructure.Repositories.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(AppDbContext context, ILogger<CompanyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all companies");
                var companies = await _context.Companies.ToListAsync();
                _logger.LogInformation("Successfully fetched all companies");
                return companies;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching companies", ex);  
            }
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching company with ID: {Id}", id);
                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    _logger.LogWarning("Company with ID {Id} not found", id);
                }
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching company with ID: {Id}", id);
                throw new Exception($"Error fetching company with ID {id}", ex);
            }
        }

        public async Task AddAsync(Company company)
        {
            try
            {
                _logger.LogInformation("Adding new company: {CompanyName}", company.CompanyName);
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added company: {CompanyName}", company.CompanyName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding new company: {CompanyName}", company.CompanyName);
                throw new Exception("Error adding company", ex);
            }
        }

        public async Task UpdateAsync(Company company)
        {
            try
            {
                _context.Companies.Update(company);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated company with ID: {CompanyId}", company.CompanyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating company with ID: {CompanyId}", company.CompanyId);
                throw new Exception($"Error updating company with ID {company.CompanyId}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                if (company != null)
                {
                    _context.Companies.Remove(company);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully deleted company with ID: {CompanyId}", id);
                }
                else
                {
                    _logger.LogWarning("Company with ID {CompanyId} not found", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting company with ID: {CompanyId}", id);
                throw new Exception($"Error deleting company with ID {id}", ex);
            }
        }
    }
}
