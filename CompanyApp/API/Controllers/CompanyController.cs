using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyService.GetAllCompanies();
                _logger.LogInformation("Fetched all companies");
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching companies");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyService.GetCompanyById(id);

                if (company == null)
                {
                    return NotFound($"Company with ID {id} not found");
                }

                _logger.LogInformation("Fetched company with ID: {Id}", id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching company");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            try
            {
                await _companyService.AddCompany(company);
                _logger.LogInformation("Created new company: {CompanyName}.", company.CompanyName);
                return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyId }, company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while creating company.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] Company company)
        {
            try
            {
                if (id != company.CompanyId)
                {
                    return BadRequest();
                }

                await _companyService.UpdateCompany(company);
                _logger.LogInformation("Updated company with ID: {Id}", id);

                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while updating company.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _companyService.DeleteCompany(id);
                _logger.LogInformation("Successfully deleted company");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while deleting company.");
            }
        }
    }
}
