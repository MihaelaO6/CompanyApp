using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Domain.Models;
using Microsoft.Extensions.Logging;
using CompanyApp.API.Controllers;
using CompanyApp.Application.Services.Interfaces;

namespace CompanyApp.Tests.ControllerTests
{
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _mockCompanyService;
        private readonly Mock<ILogger<CompanyController>> _mockLogger; 
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _mockCompanyService = new Mock<ICompanyService>();
            _mockLogger = new Mock<ILogger<CompanyController>>();  
            _controller = new CompanyController(_mockCompanyService.Object, _mockLogger.Object); 
        }

        [Fact]
        public async Task GetCompanies_ReturnsOkResult_WithCompanies()
        {
            var companies = new List<Company>
            {
                new Company { CompanyId = 1, CompanyName = "Company 1" },
                new Company { CompanyId = 2, CompanyName = "Company 2" }
            };

            _mockCompanyService.Setup(service => service.GetAllCompanies())
                               .ReturnsAsync(companies);

            var result = await _controller.GetCompanies();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCompanies = Assert.IsAssignableFrom<IEnumerable<Company>>(okResult.Value);
            Assert.Equal(2, returnCompanies.Count());
        }

        [Fact]
        public async Task GetCompanyById_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            int companyId = 999;
            _mockCompanyService.Setup(service => service.GetCompanyById(companyId))
                               .ReturnsAsync((Company)null);

            var result = await _controller.GetCompany(companyId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsOkResult_WhenCompanyExists()
        {
            int companyId = 1;
            var company = new Company { CompanyId = companyId, CompanyName = "Company 1" };
            _mockCompanyService.Setup(service => service.GetCompanyById(companyId))
                               .ReturnsAsync(company);

            var result = await _controller.GetCompany(companyId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCompany = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(companyId, returnCompany.CompanyId);
        }

        [Fact]
        public async Task AddCompany_ReturnsCreatedAtActionResult_WhenCompanyIsAdded()
        {
            var company = new Company { CompanyName = "New Company" };
            _mockCompanyService.Setup(service => service.AddCompany(company))
                               .Returns(Task.CompletedTask);

            var result = await _controller.CreateCompany(company);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetCompanyById", createdAtActionResult.ActionName);
            Assert.Equal(company, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateCompany_ReturnsNoContent_WhenCompanyIsUpdated()
        {
            var company = new Company { CompanyId = 1, CompanyName = "Updated Company" };
            _mockCompanyService.Setup(service => service.UpdateCompany(company))
                               .Returns(Task.CompletedTask);

            var result = await _controller.CreateCompany(company);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCompany_ReturnsNoContent_WhenCompanyIsDeleted()
        {
            int companyId = 1;
            _mockCompanyService.Setup(service => service.DeleteCompany(companyId))
                               .Returns(Task.CompletedTask);

            var result = await _controller.DeleteCompany(companyId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            int companyId = 999;
            _mockCompanyService.Setup(service => service.DeleteCompany(companyId))
                               .ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.DeleteCompany(companyId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
