using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProjectStatusService
{
    Task<ProjectStatusEntity> CreateProjectStatus(ProjectStatusModel projectStatus);
    Task<IEnumerable<ProjectStatusEntity>> GetAllProjectStatuses();
    Task<ProjectStatusEntity> GetProjectStatusById(int id);
    Task<ProjectStatusEntity> GetProjectStatusByName(string name);
    Task<ProjectStatusEntity> UpdateProjectStatus(int id, ProjectStatusEntity updatedProjectStatus);
    Task<bool> DeleteProjectStatus(int id);
    string ErrorMessage { get; }
}
