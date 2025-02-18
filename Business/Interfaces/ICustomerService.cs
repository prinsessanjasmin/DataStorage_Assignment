

using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<bool> CreateCustomer(CustomerModel customer);
    Task<IEnumerable<CustomerEntity>> GetAllCustomers();
    Task<CustomerEntity> GetCustomerById(int id);
    Task<CustomerEntity> GetCustomerByName(string email);
    Task<CustomerEntity> UpdateCustomer(int id, CustomerEntity updatedCustomer);
    Task<bool> DeleteCustomer(int id);
}
