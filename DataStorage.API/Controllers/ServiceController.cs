using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController(ICompanyServiceService companyServiceService) : ControllerBase
{
    private readonly ICompanyServiceService _companyServiceService = companyServiceService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyServiceEntity>>> GetCompanyServices()
    {
        try
        {
            var services = await _companyServiceService.GetAllCompanyServices();
            if (services == null)
            {
                return NotFound("Couldn't find any services in the database");
            }
            return Ok(services);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyServiceEntity>> GetCompanyServiceById(int id)
    {
        try
        {
            var service = await _companyServiceService.GetCompanyServiceById(id);
            if (service == null)
            {
                return NotFound("The service you're looking for wasn't found in the database.");
            }
            return Ok(service);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CompanyServiceEntity>> CreateCompanyService(CompanyServiceModel service)
    {
        try
        {
            var created = await _companyServiceService.CreateCompanyService(service);
            if (created == null)
            {
                return BadRequest("Failed to create service");

            }
            return CreatedAtAction(nameof(GetCompanyServiceById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyServiceEntity>> UpdateCompanyService(int id, CompanyServiceEntity updatedCompanyService)
    {
        try
        {
            var service = await _companyServiceService.UpdateCompanyService(id, updatedCompanyService);
            if (service == null)
            {
                return NotFound("The service you're trying to update wasn't found in the database.");
            }

            return Ok(service);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCompanyService(int id)
    {
        try
        {
            var service = await _companyServiceService.DeleteCompanyService(id);
            if (!service)
            {
                return NotFound("The service you're trying to delete wasn't found in the database.");
            }

            return Ok(service);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
