

using Business.Models;
using Data.Entities;
using System.Data.SqlTypes;

namespace Business.Services;

public static class HelperService
{
    public static decimal CalculateTotalPrice(ProjectEntity project)
    {
        decimal total = project.CompanyService.Price * project.Quantity;
        return total;
    }

    public static decimal CalculateTotalPrice(int quantity, decimal price)
    {
        decimal total = quantity * price;
        return total;
    }
}
