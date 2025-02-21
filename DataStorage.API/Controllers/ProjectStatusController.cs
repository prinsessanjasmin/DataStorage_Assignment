using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectStatusController(IProjectStatusService projectStatusService) : ControllerBase
{
    private readonly IProjectStatusService _projectStatusService = projectStatusService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectStatusEntity>>> GetAllProjectStatuses()
    {
        try
        {
            var projectStatuses = await _projectStatusService.GetAllProjectStatuses();
            if (projectStatuses == null)
            {
                return NotFound("Couldn't find any project statuses in the database");
            }
            return Ok(projectStatuses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectStatusEntity>> GetProjectStatusById(int id)
    {
        try
        {
            var projectStatus = await _projectStatusService.GetProjectStatusById(id);
            if (projectStatus == null)
            {
                return NotFound("The project status you're looking for wasn't found in the database.");
            }
            return Ok(projectStatus);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<ProjectStatusEntity>> GetProjectStatusByName(string name)
    {
        try
        {
            var projectStatus = await _projectStatusService.GetProjectStatusByName(name);
            if (projectStatus == null)
            {
                return NotFound("The project status you're looking for wasn't found in the database.");
            }
            return Ok(projectStatus);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProjectStatusEntity>> CreateProjectStatus(ProjectStatusModel projectStatus)
    {
        try
        {
            var created = await _projectStatusService.CreateProjectStatus(projectStatus);
            if (created == null)
            {
                return BadRequest("Failed to create project status");

            }
            return CreatedAtAction(nameof(GetProjectStatusById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectStatusEntity>> UpdateProjectStatus(int id, ProjectStatusEntity updatedProjectStatus)
    {
        try
        {
            var projectStatus = await _projectStatusService.UpdateProjectStatus(id, updatedProjectStatus);
            if (projectStatus == null)
            {
                return NotFound("The project status you're trying to update wasn't found in the database.");
            }

            return Ok(projectStatus);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProjectStatus(int id)
    {
        try
        {
            var projectStatus = await _projectStatusService.DeleteProjectStatus(id);
            if (!projectStatus)
            {
                return NotFound("The project status you're trying to delete wasn't found in the database.");
            }

            return Ok(projectStatus);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

