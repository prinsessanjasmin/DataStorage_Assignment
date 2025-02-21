using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CurrencyFactory
{
    public static CurrencyEntity Create(CurrencyModel model)
    {
        return new CurrencyEntity
        {
            CurrencyName = model.CurrencyName,
        };
    }
}
