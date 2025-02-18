
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomer(CustomerModel customer)
    {
        try
        {
            CustomerEntity customerEntity = CustomerFactory.Create(customer);
            var name = customerEntity.CustomerName;
            bool exists = await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == name);
            if (exists)
            {
                Debug.WriteLine("A customer with the same name already exists.");
                return false;
            }

            await _customerRepository.CreateAsync(customerEntity);
            await _customerRepository.SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer :: {ex.Message}");
            return false;
        }
    }



    public async Task<IEnumerable<CustomerEntity>> GetAllCustomers()
    {
        IEnumerable<CustomerEntity> list = [];

        try
        {
            list = await _customerRepository.GetAsync();
            if (list == null)
            {
                return [];
            }

            return list;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding customers :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> GetCustomerById(int id)
    {
        try
        {
            CustomerEntity customer = await _customerRepository.GetAsync(x => x.Id == id);
            if (customer == null)
                return null!;

            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding customer :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> GetCustomerByName(string name)
    {
        try
        {
            CustomerEntity customer = await _customerRepository.GetAsync(x => x.CustomerName == name);
            if (customer == null)
                return null!;

            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding customer :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> UpdateCustomer(int id, CustomerEntity updatedCustomer)
    {
        try
        {
            CustomerEntity customer = await _customerRepository.UpdateAsync(x => x.Id == id, updatedCustomer);
            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        try
        {
            bool deleted = await _customerRepository.DeleteAsync(x => x.Id == id);
            return deleted;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer :: {ex.Message}");
            return false!;
        }
    }
}
