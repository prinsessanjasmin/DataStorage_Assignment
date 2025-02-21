using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Entities;
using System.Diagnostics;

namespace Business.Services;

public class ProjectStatusService(IProjectStatusRepository projectStatusRepository) : IProjectStatusService
{
    private readonly IProjectStatusRepository _projectStatusRepository = projectStatusRepository;

    public async Task<ProjectStatusEntity> CreateProjectStatus(ProjectStatusModel projectStatus)
    {
        await _projectStatusRepository.BeginTransactionAsync();

        try
        {
            ProjectStatusEntity projectStatusEntity = ProjectStatusFactory.Create(projectStatus);
            var name = projectStatusEntity.StatusName;
            bool exists = await _projectStatusRepository.AlreadyExistsAsync(x => x.StatusName == name);
            if (exists)
            {
                Debug.WriteLine("A project status with the same name already exists.");
                return null!;
            }

            ProjectStatusEntity createdProjectStatus = await _projectStatusRepository.CreateAsync(projectStatusEntity);
            await _projectStatusRepository.SaveAsync();
            await _projectStatusRepository.CommitTransactionAsync();
            return createdProjectStatus;
        }
        catch (Exception ex)
        {
            await _projectStatusRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating project status :: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<ProjectStatusEntity>> GetAllProjectStatuses()
    {
        IEnumerable<ProjectStatusEntity> list = [];

        try
        {
            list = await _projectStatusRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project statuses :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectStatusEntity> GetProjectStatusById(int id)
    {
        try
        {
            ProjectStatusEntity projectStatus = await _projectStatusRepository.GetAsync(x => x.Id == id);
            if (projectStatus == null)
                return null!;

            return projectStatus;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project status :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectStatusEntity> GetProjectStatusByName(string name)
    {
        try
        {
            ProjectStatusEntity projectStatus = await _projectStatusRepository.GetAsync(x => x.StatusName == name);
            if (projectStatus == null)
                return null!;

            return projectStatus;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project status :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProjectStatusEntity> UpdateProjectStatus(int id, ProjectStatusEntity updatedProjectStatus)
    {
        await _projectStatusRepository.BeginTransactionAsync();

        try
        {
            ProjectStatusEntity projectStatus = await _projectStatusRepository.UpdateAsync(x => x.Id == id, updatedProjectStatus);
            await _projectStatusRepository.SaveAsync();
            await _projectStatusRepository.CommitTransactionAsync();
            return projectStatus;
        }
        catch (Exception ex)
        {
            await _projectStatusRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating project status :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteProjectStatus(int id)
    {
        await _projectStatusRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _projectStatusRepository.DeleteAsync(x => x.Id == id);
            await _projectStatusRepository.SaveAsync();
            await _projectStatusRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _projectStatusRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting project status :: {ex.Message}");
            return false!;
        }
    }

    public string ErrorMessage => throw new NotImplementedException();
}
