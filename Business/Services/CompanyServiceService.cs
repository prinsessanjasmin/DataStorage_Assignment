using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class CompanyServiceService(ICompanyServiceRepository companyServiceRepository) : ICompanyServiceService
{
    private readonly ICompanyServiceRepository _companyServiceRepository = companyServiceRepository;

    public string ErrorMessage => throw new NotImplementedException();

    public async Task<CompanyServiceEntity> CreateCompanyService(CompanyServiceModel companyServiceModel)
    {
        await _companyServiceRepository.BeginTransactionAsync();

        try
        {
            CompanyServiceEntity companyServiceEntity = CompanyServiceFactory.Create(companyServiceModel);

            var title = companyServiceEntity.Title;
            bool exists = await _companyServiceRepository.AlreadyExistsAsync(x => x.Title == title);
            if (exists)
            {
                await _companyServiceRepository.RollbackTransactionAsync();
                Debug.WriteLine("A service with the same name already exists. Use another title if you want to create a new project.");
                return null!;
            }

            await _companyServiceRepository.CreateAsync(companyServiceEntity);
            await _companyServiceRepository.SaveAsync();
            await _companyServiceRepository.CommitTransactionAsync();
            return companyServiceEntity;
        }
        catch (Exception ex)
        {
            await _companyServiceRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating service :: {ex.Message}");
            return null!;
        }
    }


    public async Task<IEnumerable<CompanyServiceEntity>> GetAllCompanyServices()
    {
        IEnumerable<CompanyServiceEntity> list = [];

        try
        {
            list = await _companyServiceRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding services :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CompanyServiceEntity> GetCompanyServiceById(int id)
    {
        try
        {
            CompanyServiceEntity service = await _companyServiceRepository.GetAsync(x => x.Id == id);
            if (service == null)
                return null!;

            return service;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding service :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CompanyServiceEntity> UpdateCompanyService(int id, CompanyServiceEntity updatedCompanyService)
    {
        await _companyServiceRepository.BeginTransactionAsync();

        try
        {
            CompanyServiceEntity service = await _companyServiceRepository.UpdateAsync(x => x.Id == id, updatedCompanyService);

            await _companyServiceRepository.SaveAsync();
            await _companyServiceRepository.CommitTransactionAsync();
            return service;
        }
        catch (Exception ex)
        {
            await _companyServiceRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating service :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteCompanyService(int id)
    {
        await _companyServiceRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _companyServiceRepository.DeleteAsync(x => x.Id == id);
            await _companyServiceRepository.SaveAsync();
            await _companyServiceRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _companyServiceRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting service :: {ex.Message}");
            return false!;
        }
    }
}
