

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ContactPersonFactory
{
    public static ContactPersonEntity Create(ContactPersonModel model)
    {
        return new ContactPersonEntity
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
        };
    }
}
