using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmplyeeController(IEmployeeService employeeService) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeEntity>>> GetEmployees()
    {
        try
        {
            var employees = await _employeeService.GetAllEmployees();
            if (employees == null)
            {
                return NotFound("Couldn't find any employees in the database");
            }
            return Ok(employees);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeEntity>> GetEmployeeById(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound("The employee you're looking for wasn't found in the database.");
            }
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<EmployeeEntity>> GetEmployeeByEmail(string email)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByEmail(email);
            if (employee == null)
            {
                return NotFound("The employee you're looking for wasn't found in the database.");
            }
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeEntity>> CreateEmployee(EmployeeModel employee)
    {
        try
        {
            var created = await _employeeService.CreateEmployee(employee);
            if (created == null)
            {
                return BadRequest("Failed to create employee");

            }
            return CreatedAtAction(nameof(GetEmployeeById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeEntity>> UpdateEmployee(int id, EmployeeEntity updatedEmployee)
    {
        try
        {
            var employee = await _employeeService.UpdateEmployee(id, updatedEmployee);
            if (employee == null)
            {
                return NotFound("The employee you're trying to update wasn't found in the database.");
            }

            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _employeeService.DeleteEmployee(id);
            if (!employee)
            {
                return NotFound("The employee you're trying to delete wasn't found in the database.");
            }

            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
