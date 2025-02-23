﻿
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System.Diagnostics;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public string ErrorMessage => throw new NotImplementedException();

    public async Task<CustomerEntity> CreateCustomer(CustomerModel customer)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        {
            CustomerEntity customerEntity = CustomerFactory.Create(customer);
            var name = customerEntity.CustomerName;
            bool exists = await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == name);
            if (exists)
            {
                Debug.WriteLine("A customer with the same name already exists.");
                return null!;
            }

            CustomerEntity createdCustomer = await _customerRepository.CreateAsync(customerEntity);
            await _customerRepository.SaveAsync();
            await _customerRepository.CommitTransactionAsync();
            return createdCustomer;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating customer :: {ex.Message}");
            return null!;
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
        await _customerRepository.BeginTransactionAsync();

        try
        {
            CustomerEntity customer = await _customerRepository.UpdateAsync(x => x.Id == id, updatedCustomer);
            await _customerRepository.SaveAsync();
            await _customerRepository.CommitTransactionAsync();
            return customer;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating project :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        await _customerRepository.BeginTransactionAsync();

        try
        {
            bool deleted = await _customerRepository.DeleteAsync(x => x.Id == id);
            await _customerRepository.SaveAsync();
            await _customerRepository.CommitTransactionAsync();
            return deleted;
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting customer :: {ex.Message}");
            return false!;
        }
    }
}
