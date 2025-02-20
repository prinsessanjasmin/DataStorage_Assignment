using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactPersonController(IContactPersonService contactPersonService) : ControllerBase
{
    private readonly IContactPersonService _contactPersonService = contactPersonService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactPersonEntity>>> GetAllContacts()
    {
        try
        {
            var contactPersons = await _contactPersonService.GetAllContacts();
            if (contactPersons == null)
            {
                return NotFound("Couldn't find any contact persons in the database");
            }
            return Ok(contactPersons);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactPersonEntity>> GetContactById(int id)
    {
        try
        {
            var contact = await _contactPersonService.GetContactById(id);
            if (contact == null)
            {
                return NotFound("The contact you're looking for wasn't found in the database.");
            }
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<ContactPersonEntity>> GetContactByEmail(string email)
    {
        try
        {
            var contact = await _contactPersonService.GetContactByEmail(email);
            if (contact == null)
            {
                return NotFound("The contact you're looking for wasn't found in the database.");
            }
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ContactPersonEntity>> CreateContact(ContactPersonModel contact)
    {
        try
        {
            var created = await _contactPersonService.CreateContact(contact);
            if (created == null)
            {
                return BadRequest("Failed to create contact");

            }
            return CreatedAtAction(nameof(GetContactById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ContactPersonEntity>> UpdateContactPerson(int id, ContactPersonEntity updatedContact)
    {
        try
        {
            var contact = await _contactPersonService.UpdateContactPerson(id, updatedContact);
            if (contact == null)
            {
                return NotFound("The contact you're trying to update wasn't found in the database.");
            }

            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteContact(int id)
    {
        try
        {
            var contact = await _contactPersonService.DeleteContact(id);
            if (!contact)
            {
                return NotFound("The contact you're trying to delete wasn't found in the database.");
            }

            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
