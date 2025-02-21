using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICurrencyService
{
    Task<CurrencyEntity> CreateCurrency(CurrencyModel currency);
    Task<IEnumerable<CurrencyEntity>> GetAllCurrencies();
    Task<CurrencyEntity> GetCurrencyById(int id);
    Task<CurrencyEntity> GetCurrencyByCurrencyName(string currencyName);
    Task<CurrencyEntity> UpdateCurrency(int id, CurrencyEntity updatedCurrency);
    Task<bool> DeleteCurrency(int id);
    string ErrorMessage { get; }
}
