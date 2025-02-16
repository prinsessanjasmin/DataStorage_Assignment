
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CompanyServiceRepository(DataContext context) : BaseRepository<CompanyServiceEntity>(context), ICompanyServiceRepository
{
    private readonly DataContext _context = context;
}
