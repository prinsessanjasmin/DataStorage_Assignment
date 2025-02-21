using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyRoleController(ICompanyRoleService companyRoleService) : ControllerBase
{
    private readonly ICompanyRoleService _companyRoleService = companyRoleService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyRoleEntity>>> GetCompanyRoles()
    {
        try
        {
            var roles = await _companyRoleService.GetAllCompanyRoles();
            if (roles == null)
            {
                return NotFound("Couldn't find any roles in the database");
            }
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyRoleEntity>> GetCompanyRoleById(int id)
    {
        try
        {
            var role = await _companyRoleService.GetCompanyRoleById(id);
            if (role == null)
            {
                return NotFound("The role you're looking for wasn't found in the database.");
            }
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<CompanyRoleEntity>> GetCompanyRoleByName(string name)
    {
        try
        {
            var role = await _companyRoleService.GetCompanyRoleByRoleName(name);
            if (role == null)
            {
                return NotFound("The role you're looking for wasn't found in the database.");
            }
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CompanyRoleEntity>> CreateCompanyRole(CompanyRoleModel role)
    {
        try
        {
            var created = await _companyRoleService.CreateCompanyRole(role);
            if (created == null)
            {
                return BadRequest("Failed to create role");

            }
            return CreatedAtAction(nameof(GetCompanyRoleById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyRoleEntity>> UpdateCompanyRole(int id, CompanyRoleEntity updatedCompanyRole)
    {
        try
        {
            var role = await _companyRoleService.UpdateCompanyRole(id, updatedCompanyRole);
            if (role == null)
            {
                return NotFound("The role you're trying to update wasn't found in the database.");
            }

            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCompanyRole(int id)
    {
        try
        {
            var role = await _companyRoleService.DeleteCompanyRole(id);
            if (!role)
            {
                return NotFound("The role you're trying to delete wasn't found in the database.");
            }

            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

