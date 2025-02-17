using Data.Interfaces;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Business.Factories;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(ProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProject(ProjectModel project)
    {
        try
        {
            
            ProjectEntity projectEntity = ProjectFactory.Create(project);
            var name = projectEntity.Title;
            bool exists = await _projectRepository.AlreadyExistsAsync(x => x.Title == name);
            if (exists)
            {
                Debug.WriteLine("A project with the same name already exists. Use another title if you want to create a new project.");
                return false; 
            }

            await _projectRepository.CreateAsync(projectEntity);
            await _projectRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating project :: {ex.Message}");
            return false;
        }
    }


    public async Task<IEnumerable<ProjectEntity>> GetAllProjects()
    {
        IEnumerable<ProjectEntity> list = [];

        try
        {
            list = await _projectRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding projects :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectById(int id)
    {
        try
        {
            ProjectEntity projectById = await _projectRepository.GetAsync(p => p.Id == id);
            if (projectById == null)
                return null!;

            return projectById;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectByProjectName(string projectName)
    {
        try
        {
            ProjectEntity projectByName = await _projectRepository.GetAsync(p => p.Title == projectName);
            if (projectByName == null)
                return null!;

            return projectByName;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> UpdateProject(int id, ProjectEntity updatedProject)
    {
        try
        {
            ProjectEntity project = await _projectRepository.UpdateAsync(x => x.Id == id, updatedProject);
            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteProject(int id)
    {
        try
        {
            bool deleted = await _projectRepository.DeleteAsync(x => x.Id == id);
            return deleted;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project :: {ex.Message}");
            return false!;
        }
    }
}
