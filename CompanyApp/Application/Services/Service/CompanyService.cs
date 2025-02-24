using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Domain.Models;
using CompanyApp.Infrastructure.Repositories.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApp.Application.Services.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            try
            {
                _logger.LogInformation("Fetching all companies");
                var companies = await _companyRepository.GetAllAsync();
                return companies;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching companies", ex);
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            try
            {
                _logger.LogInformation("Fetching company with ID: {Id}", id);
                var company = await _companyRepository.GetByIdAsync(id);
                if (company == null)
                {
                    _logger.LogWarning("Company with ID {Id} not found.", id);
                }
                return company;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching company with ID {id}", ex);
            }
        }

        public async Task AddCompany(Company company)
        {
            try
            {
                _logger.LogInformation("Adding new company: {CompanyName}", company.CompanyName);
                await _companyRepository.AddAsync(company);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding company", ex);
            }
        }

        public async Task UpdateCompany(Company company)
        {
            try
            {
                _logger.LogInformation("Updating company with ID: {Id}", company.CompanyId);
                await _companyRepository.UpdateAsync(company);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating company with ID {company.CompanyId}", ex);
            }
        }

        public async Task DeleteCompany(int id)
        {
            try
            {
                _logger.LogInformation("Deleting company with ID: {Id}", id);
                await _companyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting company with ID {id}", ex);
            }
        }
    }
}
