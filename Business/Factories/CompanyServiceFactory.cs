

using Data.Entities;

namespace Business.Factories;

public static class CompanyServiceFactory
{
    public static CompanyServiceEntity Create (string title)
    {
        return new CompanyServiceEntity
        {
            Title = title,
        };
    }
}
