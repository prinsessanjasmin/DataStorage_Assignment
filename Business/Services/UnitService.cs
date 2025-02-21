using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Data;
using System.Diagnostics;

namespace Business.Services;

public class UnitService(IUnitRepository unitRepository) : IUnitService
{
    private readonly IUnitRepository _unitRepository = unitRepository;

    public async Task<UnitEntity> CreateUnit(UnitModel unit)
    {
        await _unitRepository.BeginTransactionAsync();

        try
        {
            UnitEntity unitEntity = UnitFactory.Create(unit);
            var unitName = unitEntity.UnitName;
            bool exists = await _unitRepository.AlreadyExistsAsync(x => x.UnitName == unitName);
            if (exists)
            {
                Debug.WriteLine("A unit with the same name already exists.");
                return null!;
            }

            await _unitRepository.CreateAsync(unitEntity);
            await _unitRepository.SaveAsync();
            await _unitRepository.CommitTransactionAsync();
            return unitEntity;
        }
        catch (Exception ex)
        {
            await _unitRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating unit :: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<UnitEntity>> GetAllUnits()
    {
        IEnumerable<UnitEntity> list = [];

        try
        {
            list = await _unitRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding units :: {ex.Message}");
            return null!;
        }
    }

    public async Task<UnitEntity> GetUnitById(int id)
    {
        try
        {
            UnitEntity unit = await _unitRepository.GetAsync(x => x.Id == id);
            if (unit == null)
                return null!;

            return unit;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding unit :: {ex.Message}");
            return null!;
        }
    }

    public async Task<UnitEntity> GetUnitByUnitName(string unitName)
    {
        try
        {
            UnitEntity unit = await _unitRepository.GetAsync(x => x.UnitName == unitName);
            if (unit == null)
                return null!;

            return unit;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding unit :: {ex.Message}");
            return null!;
        }
    }

    public async Task<UnitEntity> UpdateUnit(int id, UnitEntity updatedUnit)
    {
        await _unitRepository.BeginTransactionAsync();

        try
        {
            UnitEntity unit = await _unitRepository.UpdateAsync(x => x.Id == id, updatedUnit);
            await _unitRepository.SaveAsync();
            await _unitRepository.CommitTransactionAsync();
            return unit;
        }
        catch (Exception ex)
        {
            await _unitRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating unit :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteUnit(int id)
    {
        await _unitRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _unitRepository.DeleteAsync(x => x.Id == id);
            await _unitRepository.SaveAsync();
            await _unitRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _unitRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting unit :: {ex.Message}");
            return false!;
        }
    }

    public string ErrorMessage => throw new NotImplementedException();
}
