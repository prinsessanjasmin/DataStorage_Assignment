using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class CompanyRoleService(ICompanyRoleRepository companyRoleRepository) : ICompanyRoleService
{
    private readonly ICompanyRoleRepository _companyRoleRepository = companyRoleRepository;

    public async Task<CompanyRoleEntity> CreateCompanyRole(CompanyRoleModel role)
    {
        await _companyRoleRepository.BeginTransactionAsync();

        try
        {
            CompanyRoleEntity companyRoleEntity = CompanyRoleFactory.Create(role);
            var roleName = companyRoleEntity.CompanyRole;
            bool exists = await _companyRoleRepository.AlreadyExistsAsync(x => x.CompanyRole == roleName);
            if (exists)
            {
                Debug.WriteLine("A role with the same name already exists.");
                return null!;
            }

            await _companyRoleRepository.CreateAsync(companyRoleEntity);
            await _companyRoleRepository.SaveAsync();
            await _companyRoleRepository.CommitTransactionAsync();
            return companyRoleEntity;
        }
        catch (Exception ex)
        {
            await _companyRoleRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating role :: {ex.Message}");
            return null!;
        }
    }


    public async Task<IEnumerable<CompanyRoleEntity>> GetAllCompanyRoles()
    {
        IEnumerable<CompanyRoleEntity> list = [];

        try
        {
            list = await _companyRoleRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding roles :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CompanyRoleEntity> GetCompanyRoleById(int id)
    {
        try
        {
            CompanyRoleEntity role = await _companyRoleRepository.GetAsync(x => x.Id == id);
            if (role == null)
                return null!;

            return role;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding role :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CompanyRoleEntity> GetCompanyRoleByRoleName(string roleName)
    {
        try
        {
            CompanyRoleEntity role = await _companyRoleRepository.GetAsync(x => x.CompanyRole == roleName);
            if (role == null)
                return null!;

            return role;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding role :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CompanyRoleEntity> UpdateCompanyRole(int id, CompanyRoleEntity updatedCompanyRole)
    {
        await _companyRoleRepository.BeginTransactionAsync();

        try
        {
            CompanyRoleEntity role = await _companyRoleRepository.UpdateAsync(x => x.Id == id, updatedCompanyRole);
            await _companyRoleRepository.SaveAsync();
            await _companyRoleRepository.CommitTransactionAsync();
            return role;
        }
        catch (Exception ex)
        {
            await _companyRoleRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating role :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteCompanyRole(int id)
    {
        await _companyRoleRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _companyRoleRepository.DeleteAsync(x => x.Id == id);
            await _companyRoleRepository.SaveAsync();
            await _companyRoleRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _companyRoleRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting role :: {ex.Message}");
            return false!;
        }
    }

    public string ErrorMessage => throw new NotImplementedException();
}
