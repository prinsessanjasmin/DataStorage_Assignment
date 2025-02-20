using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DataStorage.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerEntity>>> GetCustomers()
    {
        try
        {
            var customers = await _customerService.GetAllCustomers();
            if (customers == null) 
            { 
                return NotFound("Couldn't find any customers in the database");
            }
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerEntity>> GetCustomerById(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound("The customer you're looking for wasn't found in the database.");
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<CustomerEntity>> GetCustomerByName(string name)
    {
        try
        {
            var customer = await _customerService.GetCustomerByName(name);
            if (customer == null)
            {
                return NotFound("The customer you're looking for wasn't found in the database.");
            }
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CustomerEntity>> CreateCustomer(CustomerModel customer)
    {
        try
        {
            var created = await _customerService.CreateCustomer(customer);
            if (created == null)
            {
                return BadRequest("Failed to create customer");
               
            }
            return CreatedAtAction(nameof(GetCustomerById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerEntity>> UpdateCustomer(int id, CustomerEntity updatedCustomer)
    {
        try
        {
            var customer = await _customerService.UpdateCustomer(id, updatedCustomer);
            if (customer == null)
            {
                return NotFound("The customer you're trying to update wasn't found in the database.");
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCustomer(int id)
    {
        try
        {
            var customer = await _customerService.DeleteCustomer(id);
            if (!customer)
            {
                return NotFound("The customer you're trying to delete wasn't found in the database.");
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

