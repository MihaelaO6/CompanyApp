using CompanyApp.Application.Services.Interfaces;
using CompanyApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContacts();
                _logger.LogInformation("Fetched all contacts.");
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching contacts.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            try
            {
                var contact = await _contactService.GetContactById(id);

                if (contact == null)
                {
                    _logger.LogWarning("Contact with ID: {Id} not found", id);
                    return NotFound();
                }

                _logger.LogInformation("Fetched contact with ID: {Id}", id);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching contact");
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetContactsPagination(int pageNumber)
        {
            try
            {
                var contacts = await _contactService.GetPagedContacts(pageNumber);
                _logger.LogInformation("Fetched paginated contacts for page number: {PageNumber}", pageNumber);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching paginated contacts");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] Contact contact)
        {
            try
            {
                await _contactService.AddContact(contact);
                _logger.LogInformation("Created new contact: {ContactName}.", contact.ContactName);
                return CreatedAtAction(nameof(GetContact), new { id = contact.ContactId }, contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while creating contact");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            try
            {
                if (id != contact.ContactId)
                {
                    return BadRequest();
                }

                await _contactService.UpdateContact(contact);
                _logger.LogInformation("Updated contact with ID: {Id}", id);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while updating contact");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                _logger.LogInformation("Deleted contact with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while deleting contact");
            }
        }

        // GET: api/contact/details
        [HttpGet("details")]
        public async Task<IActionResult> GetContactsWithDetails()
        {
            try
            {
                var contactsWithDetails = await _contactService.GetContactsWithDetailsAsync();
                _logger.LogInformation("Fetched all contacts with details");
                return Ok(contactsWithDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while fetching contacts with details");
            }
        }

        // GET: api/contact/filter
        [HttpGet("filter")]
        public async Task<IActionResult> FilterContacts([FromQuery] int? countryId, [FromQuery] int? companyId)
        {
            try
            {
                var filteredContacts = await _contactService.FilterContactsAsync(countryId, companyId);
                _logger.LogInformation("Filtered contacts");
                return Ok(filteredContacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while filtering contacts");
            }
        }
    }
}
