using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class CurrencyApiService(HttpClient httpClient) : ICurrencyService
{
    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<CurrencyEntity> CreateCurrency(CurrencyModel currency)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Currency", currency);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CurrencyEntity>();
            }
            ErrorMessage = "Failed to create currency";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<IEnumerable<CurrencyEntity>> GetAllCurrencies()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Currency");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<CurrencyEntity>>();
            }
            ErrorMessage = "Failed to get currencies";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<CurrencyEntity> GetCurrencyByCurrencyName(string currencyName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Currency/{currencyName}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CurrencyEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find currency";
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

    public async Task<CurrencyEntity> GetCurrencyById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Currency/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CurrencyEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find currency";
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

    public async Task<CurrencyEntity> UpdateCurrency(int id, CurrencyEntity updatedCurrency)
    {

        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Currency/{id}", updatedCurrency);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<CurrencyEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update currency";
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

    public async Task<bool> DeleteCurrency(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Currency/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete currency";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }
}
