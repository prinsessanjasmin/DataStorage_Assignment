

using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity Create (string customerName)
    {
        return new CustomerEntity
        {
            CustomerName = customerName,
        };
    }
}
