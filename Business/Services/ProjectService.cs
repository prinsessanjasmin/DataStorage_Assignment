using Data.Interfaces;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Business.Factories;
using System.Diagnostics;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICompanyServiceRepository companyServiceRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICompanyServiceRepository _companyServiceRepository = companyServiceRepository;

    public string ErrorMessage => throw new NotImplementedException();

    public async Task<ProjectEntity> CreateProject(ProjectModel project)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            
            ProjectEntity projectEntity = ProjectFactory.Create(project);
            
            var name = projectEntity.Title;
            bool exists = await _projectRepository.AlreadyExistsAsync(x => x.Title == name);
            if (exists)
            {
                await _projectRepository.RollbackTransactionAsync();
                Debug.WriteLine("A project with the same name already exists. Use another title if you want to create a new project.");
                return null!; 
            }

            int cId = projectEntity.CompanyServiceId;
            CompanyServiceEntity pcse = await _companyServiceRepository.GetAsync(x => x.Id == cId);
            if (pcse == null)
            {
                Debug.WriteLine("Error: CompanyService is null. Cannot calculate TotalPrice.");
                return null!;
            }

            projectEntity.TotalPrice = projectEntity.Quantity * pcse.Price;

            await _projectRepository.CreateAsync(projectEntity);
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return projectEntity;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating project :: {ex.Message}");
            return null!;
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
            ProjectEntity project = await _projectRepository.GetAsync(x => x.Id == id);
            if (project == null)
                return null!;

            return project;
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
            ProjectEntity project = await _projectRepository.GetAsync(x => x.Title == projectName);
            if (project == null)
                return null!;

            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectByStartdate(DateTime startDate)
    {
        try
        {
            ProjectEntity project = await _projectRepository.GetAsync(x => x.Timeframe.StartDate == startDate);
            if (project == null)
                return null!;

            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectByEndDate(DateTime endDate)
    {
        try
        {
            ProjectEntity project = await _projectRepository.GetAsync(x => x.Timeframe.EndDate == endDate);
            if (project == null)
                return null!;

            return project;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectEntity> UpdateProject(int id, ProjectEntity updatedProject)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            int cId = updatedProject.CompanyServiceId;
            CompanyServiceEntity pcse = await _companyServiceRepository.GetAsync(x => x.Id == cId);
            if (pcse == null)
            {
                Debug.WriteLine("Error: CompanyService is null. Cannot calculate TotalPrice.");
                return null!;
            }

            updatedProject.TotalPrice = updatedProject.Quantity * pcse.Price;
            ProjectEntity project = await _projectRepository.UpdateAsync(x => x.Id == id, updatedProject);
            
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return project;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteProject(int id)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _projectRepository.DeleteAsync(x => x.Id == id);
            await _projectRepository.SaveAsync();
            await _projectRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting project :: {ex.Message}");
            return false!;
        }
    }

}
