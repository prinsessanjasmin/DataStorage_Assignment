using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICompanyServiceService
{
    Task<CompanyServiceEntity> CreateCompanyService(CompanyServiceModel companyService);
    Task<IEnumerable<CompanyServiceEntity>> GetAllCompanyServices();
    Task<CompanyServiceEntity> GetCompanyServiceById(int id);
    Task<CompanyServiceEntity> UpdateCompanyService(int id, CompanyServiceEntity updatedCompanyService);
    Task<bool> DeleteCompanyService(int id);
    string ErrorMessage { get; }


}
