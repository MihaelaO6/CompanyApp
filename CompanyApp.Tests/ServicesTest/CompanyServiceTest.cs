using Moq;
using Xunit;
using CompanyApp.Infrastructure.Repositories.Interface;
using CompanyApp.Domain.Models;
using CompanyApp.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Application.Services.Service;


namespace CompanyApp.Tests.Services
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly ICompanyService _companyService;
        private readonly Mock<ILogger<CompanyService>> _mockLogger;


        public CompanyServiceTests()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _companyService = new CompanyService(_mockCompanyRepository.Object, _mockLogger.Object);
            _mockLogger = new Mock<ILogger<CompanyService>>();
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsListOfCompanies()
        {
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, CompanyName = "Company One" },
                new Company { CompanyId = 2, CompanyName = "Company Two" }
            };

            _mockCompanyRepository.Setup(repo => repo.GetAllAsync())
                                  .ReturnsAsync(companies);

            var result = await _companyService.GetAllCompanies();

            var companyList = Assert.IsAssignableFrom<IEnumerable<Company>>(result);
            Assert.Equal(2, companyList.Count());
        }

        [Fact]
        public async Task GetCompanyById_ReturnsCompany_WhenCompanyExists()
        {
            int companyId = 1;
            var company = new Company { CompanyId = companyId, CompanyName = "Company One" };

            _mockCompanyRepository.Setup(repo => repo.GetByIdAsync(companyId))
                                  .ReturnsAsync(company);

            var result = await _companyService.GetCompanyById(companyId);

            var returnedCompany = Assert.IsType<Company>(result);
            Assert.Equal(companyId, returnedCompany.CompanyId);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsNull_WhenCompanyDoesNotExist()
        {
            int companyId = 999; 
            _mockCompanyRepository.Setup(repo => repo.GetByIdAsync(companyId))
                                  .ReturnsAsync((Company)null);

            var result = await _companyService.GetCompanyById(companyId);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddCompany_CreatesNewCompany()
        {
            var newCompany = new Company { CompanyName = "New Company" };

            _mockCompanyRepository.Setup(repo => repo.AddAsync(It.IsAny<Company>()))
                                  .Returns(Task.CompletedTask);

            await _companyService.AddCompany(newCompany);

            _mockCompanyRepository.Verify(repo => repo.AddAsync(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCompany_UpdatesExistingCompany()
        {
            var companyToUpdate = new Company { CompanyId = 1, CompanyName = "Updated Company" };

            _mockCompanyRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Company>()))
                                  .Returns(Task.CompletedTask);

            await _companyService.UpdateCompany(companyToUpdate);

            _mockCompanyRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Company>()), Times.Once);
        }


        [Fact]
        public async Task DeleteCompany_DeletesCompany_WhenCompanyExists()
        {
            int companyId = 1;
            _mockCompanyRepository.Setup(repo => repo.DeleteAsync(companyId))
                                  .Returns(Task.CompletedTask);

            await _companyService.DeleteCompany(companyId);

            _mockCompanyRepository.Verify(repo => repo.DeleteAsync(companyId), Times.Once);
        }

        [Fact]
        public async Task DeleteCompany_ThrowsException_WhenCompanyDoesNotExist()
        {
            int companyId = 999; 
            _mockCompanyRepository.Setup(repo => repo.DeleteAsync(companyId))
                                  .ThrowsAsync(new KeyNotFoundException());

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _companyService.DeleteCompany(companyId));
        }


        [Fact]
        public async Task AddCompany_ThrowsException_WhenRepositoryThrows()
        {
            var newCompany = new Company { CompanyName = "New Company" };
            _mockCompanyRepository.Setup(repo => repo.AddAsync(It.IsAny<Company>()))
                                  .ThrowsAsync(new Exception("Database error"));

            var exception = await Assert.ThrowsAsync<Exception>(() => _companyService.AddCompany(newCompany));
            Assert.Equal("Database error", exception.Message);
        }

        [Fact]
        public async Task UpdateCompany_ThrowsException_WhenRepositoryThrows()
        {
            var companyToUpdate = new Company { CompanyId = 1, CompanyName = "Updated Company" };
            _mockCompanyRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Company>()))
                                  .ThrowsAsync(new Exception("Database error"));

            var exception = await Assert.ThrowsAsync<Exception>(() => _companyService.UpdateCompany(companyToUpdate));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
