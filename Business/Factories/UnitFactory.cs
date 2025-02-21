using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UnitFactory
{
    public static UnitEntity Create(UnitModel model)
    {
        return new UnitEntity
        {
            UnitName = model.UnitName
        };
    }
}
