

using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class ContactPersonService(ContactPersonRepository contactPersonRepository) : IContactPersonService
{
    private readonly IContactPersonRepository _contactPersonRepository = contactPersonRepository;

    public async Task<ContactPersonEntity> CreateContact(ContactPersonModel contact)
    {
        await _contactPersonRepository.BeginTransactionAsync();

        try
        {
            ContactPersonEntity contactPersonEntity = ContactPersonFactory.Create(contact);
            var email = contactPersonEntity.Email;
            bool exists = await _contactPersonRepository.AlreadyExistsAsync(x => x.Email == email);
            if (exists)
            {
                Debug.WriteLine("A contact person with the same email already exists.");
                return null!;
            }

            await _contactPersonRepository.CreateAsync(contactPersonEntity);
            await _contactPersonRepository.SaveAsync();
            await _contactPersonRepository.CommitTransactionAsync();
            return contactPersonEntity;
        }
        catch (Exception ex)
        {
            await _contactPersonRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating contact person :: {ex.Message}");
            return null!;
        }
    }

    

    public async Task<IEnumerable<ContactPersonEntity>> GetAllContacts()
    {
        IEnumerable<ContactPersonEntity> list = [];

        try
        {
            list = await _contactPersonRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding contact persons :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ContactPersonEntity> GetContactByEmail(string email)
    {
        try
        {
            ContactPersonEntity contact = await _contactPersonRepository.GetAsync(x => x.Email == email);
            if (contact == null)
                return null!;

            return contact;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding contact person :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ContactPersonEntity> GetContactById(int id)
    {
        try
        {
            ContactPersonEntity contact = await _contactPersonRepository.GetAsync(x => x.Id == id);
            if (contact == null)
                return null!;

            return contact;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding contact person :: {ex.Message}");
            return null!;
        }
    }

    public async Task<ContactPersonEntity> UpdateContactPerson(int id, ContactPersonEntity updatedContact)
    {
        await _contactPersonRepository.BeginTransactionAsync();

        try
        {
            ContactPersonEntity contact = await _contactPersonRepository.UpdateAsync(x => x.Id == id, updatedContact);
            await _contactPersonRepository.SaveAsync();
            await _contactPersonRepository.CommitTransactionAsync();
            return contact;
        }
        catch (Exception ex)
        {
            await _contactPersonRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating contact person :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteContact(int id)
    {
        await _contactPersonRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _contactPersonRepository.DeleteAsync(x => x.Id == id);
            await _contactPersonRepository.SaveAsync();
            await _contactPersonRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _contactPersonRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting contact person :: {ex.Message}");
            return false!;
        }
    }
}
