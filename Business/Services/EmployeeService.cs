
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class EmployeeService(EmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<bool> CreateEmployee(EmployeeModel employee)
    {
        try
        {
            EmployeeEntity employeeEntity = EmployeeFactory.Create(employee);
            var email = employeeEntity.Email;
            bool exists = await _employeeRepository.AlreadyExistsAsync(x => x.Email == email);
            if (exists)
            {
                Debug.WriteLine("An employee with the same email already exists.");
                return false;
            }

            await _employeeRepository.CreateAsync(employeeEntity);
            await _employeeRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating employee :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<EmployeeEntity>> GetAllEmployees()
    {
        IEnumerable<EmployeeEntity> list = [];

        try
        {
            list = await _employeeRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding employees :: {ex.Message}");
            return null!;
        }
    }

    public async Task<EmployeeEntity> GetEmployeeByEmail(string email)
    {
        try
        {
            EmployeeEntity employee = await _employeeRepository.GetAsync(x => x.Email == email);
            if (employee == null)
                return null!;

            return employee;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding employee :: {ex.Message}");
            return null!;
        }
    }

    public async Task<EmployeeEntity> GetEmployeeById(int id)
    {
        try
        {
            EmployeeEntity employee = await _employeeRepository.GetAsync(x => x.Id == id);
            if (employee == null)
                return null!;

            return employee;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding employee :: {ex.Message}");
            return null!;
        }
    }

    public async Task<EmployeeEntity> UpdateEmployee(int id, EmployeeEntity updatedEmployee)
    {
        try
        {
            EmployeeEntity employee = await _employeeRepository.UpdateAsync(x => x.Id == id, updatedEmployee);
            return employee;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating employee :: {ex.Message}");
            return null!;
        }
    }
    public async Task<bool> DeleteEmployee(int id)
    {
        try
        {
            bool deleted = await _employeeRepository.DeleteAsync(x => x.Id == id);
            return deleted;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting employee :: {ex.Message}");
            return false!;
        }
    }

}
