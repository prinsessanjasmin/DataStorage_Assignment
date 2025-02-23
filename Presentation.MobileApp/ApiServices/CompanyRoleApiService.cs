using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class CompanyRoleApiService(HttpClient httpClient) : ICompanyRoleService
{
    private readonly HttpClient _httpClient = httpClient;

    public string ErrorMessage { get; private set; } = null!;

    public async Task<CompanyRoleEntity> CreateCompanyRole(CompanyRoleModel role)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/CompanyRole", role);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyRoleEntity>();
            }
            ErrorMessage = "Failed to create role";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<IEnumerable<CompanyRoleEntity>> GetAllCompanyRoles()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/CompanyRole");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<CompanyRoleEntity>>();
            }
            ErrorMessage = "Failed to get roles";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<CompanyRoleEntity> GetCompanyRoleById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/CompanyRole/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyRoleEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find role";
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

    public async Task<CompanyRoleEntity> GetCompanyRoleByRoleName(string roleName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/CompanyRole/{roleName}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyRoleEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find role";
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

    public async Task<CompanyRoleEntity> UpdateCompanyRole(int id, CompanyRoleEntity updatedCompanyRole)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/CompanyRole/{id}", updatedCompanyRole);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyRoleEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update role";
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

    public async Task<bool> DeleteCompanyRole(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/CompanyRole/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete role";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}

