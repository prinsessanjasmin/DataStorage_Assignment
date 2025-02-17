
using Data.Entities;

namespace Business.Factories;

public static class ProjectManagerFactory
{
    public static EmployeeEntity Create (string firstName, string lastName)
    {
        return new EmployeeEntity
        {
            FirstName = firstName,
            LastName = lastName,
        };
    }
}
