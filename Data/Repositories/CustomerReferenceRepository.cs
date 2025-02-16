
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CustomerReferenceRepository(DataContext context) : BaseRepository<CustomerReferenceEntity>(context), ICustomerReferenceRepository
{
    private readonly DataContext _context = context;
}
