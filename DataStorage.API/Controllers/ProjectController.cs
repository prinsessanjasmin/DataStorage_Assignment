using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataStorage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectEntity>>> GetProjects()
    {
        try
        {
            var projects = await _projectService.GetAllProjects();
            if (projects == null)
            {
                return NotFound("Couldn't find any projects in the database");
            }
            return Ok(projects);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectEntity>> GetProjectById(int id)
    {
        try
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound("The project you're looking for wasn't found in the database.");
            }
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("projectName/{projectName}")]
    public async Task<ActionResult<ProjectEntity>> GetProjectByProjectName(string projectName)
    {
        try
        {
            var project = await _projectService.GetProjectByProjectName(projectName);
            if (project == null)
            {
                return NotFound("The project you're looking for wasn't found in the database.");
            }
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("startDate/{startDate}")]
    public async Task<ActionResult<ProjectEntity>> GetProjectByProjectName(DateTime startDate)
    {
        try
        {
            var project = await _projectService.GetProjectByStartdate(startDate);
            if (project == null)
            {
                return NotFound("The project you're looking for wasn't found in the database.");
            }
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("endDate/{endDate}")]
    public async Task<ActionResult<ProjectEntity>> GetProjectByEndDate(DateTime endDate)
    {
        try
        {
            var project = await _projectService.GetProjectByEndDate(endDate);
            if (project == null)
            {
                return NotFound("The project you're looking for wasn't found in the database.");
            }
            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProjectEntity>> CreateProject(ProjectModel project)
    {
        try
        {
            var created = await _projectService.CreateProject(project);
            if (created == null)
            {
                return BadRequest("Failed to create project");

            }
            return CreatedAtAction(nameof(GetProjectById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectEntity>> UpdateProject(int id, ProjectEntity updatedproject)
    {
        try
        {
            var project = await _projectService.UpdateProject(id, updatedproject);
            if (project == null)
            {
                return NotFound("The project you're trying to update wasn't found in the database.");
            }

            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Deleteproject(int id)
    {
        try
        {
            var project = await _projectService.DeleteProject(id);
            if (!project)
            {
                return NotFound("The project you're trying to delete wasn't found in the database.");
            }

            return Ok(project);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
