

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CompanyServiceFactory
{
    public static CompanyServiceEntity Create (CompanyServiceModel model)
    {
        return new CompanyServiceEntity
        {
            Title = model.Title,
            Price = model.Price,
            UnitId = model.UnitId,
            CurrencyId = model.CurrencyId,
        };
    }
}
