
using Data.Entities;
using Data.Contexts;
using Data.Interfaces;

namespace Data.Repositories;

public class CompanyRoleRepository(DataContext context) : BaseRepository<CompanyRoleEntity>(context), ICompanyRoleRepository
{
    private readonly DataContext _context = context;
}
