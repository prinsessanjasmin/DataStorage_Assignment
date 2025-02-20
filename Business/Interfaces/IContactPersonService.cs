

using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IContactPersonService
{
    Task<ContactPersonEntity> CreateContact(ContactPersonModel contact);
    Task<IEnumerable<ContactPersonEntity>> GetAllContacts();
    Task<ContactPersonEntity> GetContactById(int id);
    Task<ContactPersonEntity> GetContactByEmail(string email);
    Task<ContactPersonEntity> UpdateContactPerson(int id, ContactPersonEntity updatedContact);
    Task<bool> DeleteContact(int id);
}
