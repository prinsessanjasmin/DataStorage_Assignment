using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class TimeframeApiService(HttpClient httpClient) : ITimeframeService
{
    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<TimeframeEntity> CreateTimeframe(TimeframeModel timeframe)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Timeframe", timeframe);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<TimeframeEntity>();
                
            }
            ErrorMessage = "Failed to create timeframe";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<IEnumerable<TimeframeEntity>> GetAllTimeframes()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Timeframe");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<TimeframeEntity>>();
            }
            ErrorMessage = "Failed to get timeframes";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<TimeframeEntity> GetTimeframeById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Timeframe/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<TimeframeEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find timeframe";
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

    public async Task<TimeframeEntity> UpdateTimeframe(int id, TimeframeEntity timeframe)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Timeframe/{id}", timeframe);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<TimeframeEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update timeframe";
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

    public async Task<bool> DeleteTimeframe(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Timeframe/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete timeframe";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}
