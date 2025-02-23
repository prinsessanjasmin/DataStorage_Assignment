using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class UnitApiService(HttpClient httpClient) : IUnitService
{
    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<UnitEntity> CreateUnit(UnitModel unit)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Unit", unit);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<UnitEntity>();
            }
            ErrorMessage = "Failed to create unit";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

   

    public async Task<IEnumerable<UnitEntity>> GetAllUnits()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Unit");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<UnitEntity>>();
            }
            ErrorMessage = "Failed to get units";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<UnitEntity> GetUnitById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Unit/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<UnitEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find unit";
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

    public async Task<UnitEntity> GetUnitByUnitName(string unitName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Unit/{unitName}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<UnitEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find unit";
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

    public async Task<UnitEntity> UpdateUnit(int id, UnitEntity updatedUnit)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Unit/{id}", updatedUnit);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<UnitEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update unit";
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

    public async Task<bool> DeleteUnit(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Unit/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete unit";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}
