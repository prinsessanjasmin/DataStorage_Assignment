using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeframeController(ITimeframeService timeFrameService) : ControllerBase
{
    private readonly ITimeframeService _timeframeService = timeFrameService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TimeframeEntity>>> GetTimeframes()
    {
        try
        {
            var timeframes = await _timeframeService.GetAllTimeframes();
            if (timeframes == null)
            {
                return NotFound("Couldn't find any timeframes in the database");
            }
            return Ok(timeframes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeframeEntity>> GetTimeframeById(int id)
    {
        try
        {
            var timeframe = await _timeframeService.GetTimeframeById(id);
            if (timeframe == null)
            {
                return NotFound("The timeframe you're looking for wasn't found in the database.");
            }
            return Ok(timeframe);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TimeframeEntity>> CreateTimeframe(TimeframeModel timeframe)
    {
        try
        {
            var created = await _timeframeService.CreateTimeframe(timeframe);
            if (created == null)
            {
                return BadRequest("Failed to create timeframe");

            }
            return CreatedAtAction(nameof(GetTimeframeById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TimeframeEntity>> UpdateTimeframe(int id, TimeframeEntity updatedTimeframe)
    {
        try
        {
            var timeframe = await _timeframeService.UpdateTimeframe(id, updatedTimeframe);
            if (timeframe == null)
            {
                return NotFound("The timeframe you're trying to update wasn't found in the database.");
            }

            return Ok(timeframe);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteTimeframe(int id)
    {
        try
        {
            var timeframe = await _timeframeService.DeleteTimeframe(id);
            if (!timeframe)
            {
                return NotFound("The timeframe you're trying to delete wasn't found in the database.");
            }

            return Ok(timeframe);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

