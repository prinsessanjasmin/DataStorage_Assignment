using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class ProjectApiService(HttpClient httpClient) : IProjectService
{
    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<ProjectEntity> CreateProject(ProjectModel project)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Project", project);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectEntity>();
            }
            ErrorMessage = "Failed to create project";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjects()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Project");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProjectEntity>>();
            }
            ErrorMessage = "Failed to get projects";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectByEndDate(DateTime endDate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Project/{endDate}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find project";
                return null!;
            }
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Project/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find project";
                return null!;
            }
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectByProjectName(string projectName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Project/{projectName}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find project";
                return null!;
            }
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ProjectEntity> GetProjectByStartdate(DateTime startDate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Project/{startDate}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find project";
                return null!;
            }
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ProjectEntity> UpdateProject(int id, ProjectEntity updatedProject)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Project/{id}", updatedProject);

            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                ErrorMessage = $"Failed to update project: {errorDetails}";
                return null!;
            }
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<bool> DeleteProject(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Project/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete project";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}

