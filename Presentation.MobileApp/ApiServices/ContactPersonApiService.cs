using Business.Interfaces;
using Business.Models;
using Data.Entities;
using System.Net.Http.Json;

namespace Presentation.MobileApp.ApiServices;

public class ContactPersonApiService(HttpClient httpClient) : IContactPersonService
{
    private readonly HttpClient _httpClient = httpClient;
    public string ErrorMessage { get; private set; } = null!;

    public async Task<ContactPersonEntity> CreateContact(ContactPersonModel contact)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/ContactPerson", contact);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ContactPersonEntity>();
            }
            ErrorMessage = "Failed to create contact";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }


    public async Task<IEnumerable<ContactPersonEntity>> GetAllContacts()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/ContactPerson");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<IEnumerable<ContactPersonEntity>>();
            }
            ErrorMessage = "Failed to get contacts";
            return null!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return null!;
        }
    }

    public async Task<ContactPersonEntity> GetContactByEmail(string email)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ContactPerson/{email}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ContactPersonEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find contact";
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

    public async Task<ContactPersonEntity> GetContactById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ContactPerson/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ContactPersonEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to find contact";
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

    public async Task<ContactPersonEntity> UpdateContactPerson(int id, ContactPersonEntity updatedContact)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ContactPerson/{id}", updatedContact);
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return await response.Content.ReadFromJsonAsync<ContactPersonEntity>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ErrorMessage = "Failed to update contact";
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

    public async Task<bool> DeleteContact(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ContactPerson/{id}");
            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = null!;
                return true;
            }
            ErrorMessage = "Failed to delete contact";
            return false!;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Network error: {ex.Message}";
            return false;
        }
    }

}
