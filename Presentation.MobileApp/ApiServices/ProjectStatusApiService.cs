using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Presentation.MobileApp.ApiServices;

public class ProjectStatusApiService(HttpClient httpClient) : IProjectStatusService
{
    private readonly HttpClient _httpClient = httpClient;

    public string ErrorMessage { get; private set; } = null!;

    public async Task<ProjectStatusEntity> CreateProjectStatus(ProjectStatusModel projectStatus)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/ProjectStatus", projectStatus);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();
            }
            ErrorMessage = "Failed to create project status";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }



    public async Task<IEnumerable<ProjectStatusEntity>> GetAllProjectStatuses()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/ProjectStatus");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProjectStatusEntity>>();
            }
            ErrorMessage = "Failed to get project statuses";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ProjectStatusEntity> GetProjectStatusById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ProjectStatus/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find project status";
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

    public async Task<ProjectStatusEntity> GetProjectStatusByName(string name)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ProjectStatus/{name}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find project status";
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

    public async Task<ProjectStatusEntity> UpdateProjectStatus(int id, ProjectStatusEntity updatedProjectStatus)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ProjectStatus/{id}", updatedProjectStatus);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ProjectStatusEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update project status";
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

    public async Task<bool> DeleteProjectStatus(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ProjectStatus/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete project status";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}
