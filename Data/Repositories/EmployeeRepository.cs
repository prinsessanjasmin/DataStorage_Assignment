

using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeEntity>(context), IEmployeeRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<EmployeeEntity>> GetAsync()
    {
        try
        {
            var employeeList = await _context.Employees
                .Include(x => x.CompanyRole)
                .ToListAsync();

            return employeeList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding employees :: {ex.Message}");
            return null!;
        }
    }

    public override async Task<EmployeeEntity> GetAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var employee = await _context.Employees
                .Include(x => x.CompanyRole)
                .FirstOrDefaultAsync(expression);

            return employee!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding employee :: {ex.Message}");
            return null!;
        }
    }
}
