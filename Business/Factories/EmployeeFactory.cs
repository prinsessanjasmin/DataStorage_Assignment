

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeEntity Create(EmployeeModel model)
    {
        return new EmployeeEntity
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            CompanyRoleId = model.CompanyRoleId,
        };
    }
}
