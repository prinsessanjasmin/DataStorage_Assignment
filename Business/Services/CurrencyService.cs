using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Data;
using System.Diagnostics;

namespace Business.Services;

public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;


    public async Task<CurrencyEntity> CreateCurrency(CurrencyModel currency)
    {
        await _currencyRepository.BeginTransactionAsync();

        try
        {
            CurrencyEntity currencyEntity = CurrencyFactory.Create(currency);
            var currencyName = currencyEntity.CurrencyName;
            bool exists = await _currencyRepository.AlreadyExistsAsync(x => x.CurrencyName == currencyName);
            if (exists)
            {
                Debug.WriteLine("A currency with the same name already exists.");
                return null!;
            }

            await _currencyRepository.CreateAsync(currencyEntity);
            await _currencyRepository.SaveAsync();
            await _currencyRepository.CommitTransactionAsync();
            return currencyEntity;
        }
        catch (Exception ex)
        {
            await _currencyRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating currency :: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<CurrencyEntity>> GetAllCurrencies()
    {
        IEnumerable<CurrencyEntity> list = [];

        try
        {
            list = await _currencyRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding currencies :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CurrencyEntity> GetCurrencyByCurrencyName(string currencyName)
    {
        try
        {
            CurrencyEntity currency = await _currencyRepository.GetAsync(x => x.CurrencyName == currencyName);
            if (currency == null)
                return null!;

            return currency;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding currency :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CurrencyEntity> GetCurrencyById(int id)
    {
        try
        {
            CurrencyEntity currency = await _currencyRepository.GetAsync(x => x.Id == id);
            if (currency == null)
                return null!;

            return currency;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding currency :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CurrencyEntity> UpdateCurrency(int id, CurrencyEntity updatedCurrency)
    {
        await _currencyRepository.BeginTransactionAsync();

        try
        {
            CurrencyEntity currency = await _currencyRepository.UpdateAsync(x => x.Id == id, updatedCurrency);
            await _currencyRepository.SaveAsync();
            await _currencyRepository.CommitTransactionAsync();
            return currency;
        }
        catch (Exception ex)
        {
            await _currencyRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating currency :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteCurrency(int id)
    {
        await _currencyRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _currencyRepository.DeleteAsync(x => x.Id == id);
            await _currencyRepository.SaveAsync();
            await _currencyRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _currencyRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting currency :: {ex.Message}");
            return false!;
        }
    }

    public string ErrorMessage => throw new NotImplementedException();
}
