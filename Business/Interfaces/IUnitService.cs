using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IUnitService
{
    Task<UnitEntity> CreateUnit(UnitModel unit);
    Task<IEnumerable<UnitEntity>> GetAllUnits();
    Task<UnitEntity> GetUnitById(int id);
    Task<UnitEntity> GetUnitByUnitName(string unitName);
    Task<UnitEntity> UpdateUnit(int id, UnitEntity updatedUnit);
    Task<bool> DeleteUnit(int id);
    string ErrorMessage { get; }
}
