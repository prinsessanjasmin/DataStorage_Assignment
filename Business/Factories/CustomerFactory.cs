using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity Create (CustomerModel model)
    {
        return new CustomerEntity
        {
            CustomerName = model.CustomerName
        };
    }
}
