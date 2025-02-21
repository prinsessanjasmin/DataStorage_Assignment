using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController(ICurrencyService currencyService) : ControllerBase
{
    private readonly ICurrencyService _currencyService = currencyService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrencyEntity>>> GetAllCurrencies()
    {
        try
        {
            var currencies = await _currencyService.GetAllCurrencies();
            if (currencies == null)
            {
                return NotFound("Couldn't find any currencies in the database");
            }
            return Ok(currencies);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CurrencyEntity>> GetCurrencyById(int id)
    {
        try
        {
            var currency = await _currencyService.GetCurrencyById(id);
            if (currency == null)
            {
                return NotFound("The currency you're looking for wasn't found in the database.");
            }
            return Ok(currency);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<CurrencyEntity>> GetCurrencyByName(string name)
    {
        try
        {
            var currency = await _currencyService.GetCurrencyByCurrencyName(name);
            if (currency == null)
            {
                return NotFound("The currency you're looking for wasn't found in the database.");
            }
            return Ok(currency);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CurrencyEntity>> CreateCurrency(CurrencyModel currency)
    {
        try
        {
            var created = await _currencyService.CreateCurrency(currency);
            if (created == null)
            {
                return BadRequest("Failed to create currency");

            }
            return CreatedAtAction(nameof(GetCurrencyById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CurrencyEntity>> UpdateCurrency(int id, CurrencyEntity updatedCurrency)
    {
        try
        {
            var currency = await _currencyService.UpdateCurrency(id, updatedCurrency);
            if (currency == null)
            {
                return NotFound("The currency you're trying to update wasn't found in the database.");
            }

            return Ok(currency);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteCurrency(int id)
    {
        try
        {
            var currency = await _currencyService.DeleteCurrency(id);
            if (!currency)
            {
                return NotFound("The currency you're trying to delete wasn't found in the database.");
            }

            return Ok(currency);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
