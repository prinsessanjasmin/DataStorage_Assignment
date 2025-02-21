using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController(IUnitService unitService) : ControllerBase
{
    private readonly IUnitService _unitService = unitService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitEntity>>> GetUnits()
    {
        try
        {
            var units = await _unitService.GetAllUnits();
            if (units == null)
            {
                return NotFound("Couldn't find any units in the database");
            }
            return Ok(units);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitEntity>> GetUnitById(int id)
    {
        try
        {
            var unit = await _unitService.GetUnitById(id);
            if (unit == null)
            {
                return NotFound("The unit you're looking for wasn't found in the database.");
            }
            return Ok(unit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<UnitEntity>> GetUnitByUnitName(string name)
    {
        try
        {
            var unit = await _unitService.GetUnitByUnitName(name);
            if (unit == null)
            {
                return NotFound("The unit you're looking for wasn't found in the database.");
            }
            return Ok(unit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<UnitEntity>> CreateUnit(UnitModel unit)
    {
        try
        {
            var created = await _unitService.CreateUnit(unit);
            if (created == null)
            {
                return BadRequest("Failed to create unit");

            }
            return CreatedAtAction(nameof(GetUnitById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UnitEntity>> UpdateUnit(int id, UnitEntity updatedUnit)
    {
        try
        {
            var unit = await _unitService.UpdateUnit(id, updatedUnit);
            if (unit == null)
            {
                return NotFound("The unit you're trying to update wasn't found in the database.");
            }

            return Ok(unit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteUnit(int id)
    {
        try
        {
            var unit = await _unitService.DeleteUnit(id);
            if (!unit)
            {
                return NotFound("The unit you're trying to delete wasn't found in the database.");
            }

            return Ok(unit);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
