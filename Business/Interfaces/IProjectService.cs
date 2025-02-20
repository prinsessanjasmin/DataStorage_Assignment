using Data.Entities;
using Business.Models;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<ProjectEntity> CreateProject(ProjectModel project); 
    Task<IEnumerable<ProjectEntity>> GetAllProjects();
    Task<ProjectEntity> GetProjectById(int id);
    Task<ProjectEntity> GetProjectByProjectName(string projectName);
    Task<ProjectEntity> GetProjectByStartdate(DateTime startDate);
    Task<ProjectEntity> GetProjectByEndDate(DateTime endDate);
    Task<ProjectEntity> UpdateProject(int id, ProjectEntity updatedProject);
    Task<bool> DeleteProject(int id);
}