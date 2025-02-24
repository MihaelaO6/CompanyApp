using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _countryService.GetCountries();
                _logger.LogInformation("Fetched all countries");
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching countries");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _countryService.GetCountryById(id);

                if (country == null)
                {
                    _logger.LogWarning("Country with ID: {Id} not found", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched country with ID: {Id}", id);
                return Ok(country);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching country");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] Country country)
        {
            try
            {
                await _countryService.AddCountry(country);
                _logger.LogInformation("Created new country: {CountryName}", country.CountryName);
                return CreatedAtAction(nameof(GetCountry), new { id = country.CountryId }, country);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while creating country");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] Country country)
        {
            try
            {
                if (id != country.CountryId)
                {
                    _logger.LogWarning("ID does not match Country ID", id, country.CountryId);
                    return BadRequest();
                }
                await _countryService.UpdateCountry(country);
                _logger.LogInformation("Updated country with ID: {Id}", id);

                return Ok(country);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while updating country");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                await _countryService.DeleteCountry(id);
                _logger.LogInformation("Deleted country with ID: {Id}", id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while deleting country.");
            }
        }
    }
}
