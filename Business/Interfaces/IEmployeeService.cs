

using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeEntity> CreateEmployee(EmployeeModel employee);
    Task<IEnumerable<EmployeeEntity>> GetAllEmployees();
    Task<EmployeeEntity> GetEmployeeById(int id);
    Task<EmployeeEntity> GetEmployeeByEmail(string email);
    Task<EmployeeEntity> UpdateEmployee(int id, EmployeeEntity updatedEmployee);
    Task<bool> DeleteEmployee(int id);
    string ErrorMessage { get; }
}
