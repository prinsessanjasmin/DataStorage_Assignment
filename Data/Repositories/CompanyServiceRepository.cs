
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class CompanyServiceRepository(DataContext context) : BaseRepository<CompanyServiceEntity>(context), ICompanyServiceRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<CompanyServiceEntity>> GetAsync()
    {
        try
        {
            return await _context.Services
                .Include(x => x.Currency)
                .Include(x => x.Unit)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding services :: {ex.Message}");
            return null!;
        }
    }

    public override async Task<CompanyServiceEntity> GetAsync(Expression<Func<CompanyServiceEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var service = await _context.Services
            .Include(x => x.Currency)
            .Include(x => x.Unit)

            .FirstOrDefaultAsync(expression);
            return service!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding service :: {ex.Message}");
            return null!;
        }
    }
}
