using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;
public class CompanyServiceApiService(HttpClient httpClient) : ICompanyServiceService
{
    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<CompanyServiceEntity> CreateCompanyService(CompanyServiceModel companyService)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/companyService", companyService);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyServiceEntity>();
            }
            ErrorMessage = "Failed to create service";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<IEnumerable<CompanyServiceEntity>> GetAllCompanyServices()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/companyService");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<CompanyServiceEntity>>();
            }
            ErrorMessage = "Failed to get services";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<CompanyServiceEntity> GetCompanyServiceById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/companyService/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyServiceEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find service";
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

    public async Task<CompanyServiceEntity> UpdateCompanyService(int id, CompanyServiceEntity updatedCompanyService)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customerService/{id}", updatedCompanyService);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CompanyServiceEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update service";
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

    public async Task<bool> DeleteCompanyService(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/customerService/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete service";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}
