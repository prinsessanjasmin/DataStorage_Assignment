using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICompanyRoleService
{
    Task<CompanyRoleEntity> CreateCompanyRole(CompanyRoleModel role);
    Task<IEnumerable<CompanyRoleEntity>> GetAllCompanyRoles();
    Task<CompanyRoleEntity> GetCompanyRoleById(int id);
    Task<CompanyRoleEntity> GetCompanyRoleByRoleName(string roleName);
    Task<CompanyRoleEntity> UpdateCompanyRole(int id, CompanyRoleEntity updatedCompanyRole);
    Task<bool> DeleteCompanyRole(int id);
    string ErrorMessage { get; }
}
