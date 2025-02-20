using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class EmployeeApiService(HttpClient httpClient) : IEmployeeService
{

    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<EmployeeEntity> CreateEmployee(EmployeeModel employee)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/employee", employee);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<EmployeeEntity>();
            }
            ErrorMessage = "Failed to create employee";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<IEnumerable<EmployeeEntity>> GetAllEmployees()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/employee");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeEntity>>();
            }
            ErrorMessage = "Failed to get employees";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<EmployeeEntity> GetEmployeeByEmail(string email)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/employee/{email}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<EmployeeEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find employee";
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

    public async Task<EmployeeEntity> GetEmployeeById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<EmployeeEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find employee";
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

    public async Task<EmployeeEntity> UpdateEmployee(int id, EmployeeEntity updatedEmployee)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/employee/{id}", updatedEmployee);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<EmployeeEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update employee";
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
    public async Task<bool> DeleteEmployee(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete employee";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }

}
