using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<CustomerEntity> CreateCustomer(CustomerModel customer);
    Task<IEnumerable<CustomerEntity>> GetAllCustomers();
    Task<CustomerEntity> GetCustomerById(int id);
    Task<CustomerEntity> GetCustomerByName(string name);
    Task<CustomerEntity> UpdateCustomer(int id, CustomerEntity updatedCustomer);
    Task<bool> DeleteCustomer(int id);
}
