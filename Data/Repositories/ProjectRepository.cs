

using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override Task<bool> AlreadyExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return base.AlreadyExistsAsync(expression);
    }

    public override Task BeginTransactionAsync()
    {
        return base.BeginTransactionAsync();
    }

    public override Task CommitTransactionAsync()
    {
        return base.CommitTransactionAsync();
    }

    public override Task<ProjectEntity> CreateAsync(ProjectEntity entity)
    {
        return base.CreateAsync(entity);
    }

    public override Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return base.DeleteAsync(expression);
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override async Task<IEnumerable<ProjectEntity>> GetAsync()
    {
        try
        {
            return await _context.Projects
            .Include(x => x.Timeframe)
            .Include(x => x.ProjectStatus)
            .Include(x => x.Customer)
            .Include(x => x.ProjectManager)
            .Include(x => x.CompanyService)
                .ThenInclude(x => x.Unit)
            .Include(x => x.CompanyService)
                .ThenInclude(x => x.Currency)
            .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding projects :: {ex.Message}");
            return null!;
        }

    }

    public override async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!; 

        try
        {
            var project = await _context.Projects
            .Include(x => x.Timeframe)
            .Include(x => x.ProjectStatus)
            .Include(x => x.Customer)
            .Include(x => x.ProjectManager)
            .Include(x => x.CompanyService)
                .ThenInclude(x => x.Unit)
            .Include(x => x.CompanyService)
                .ThenInclude(x => x.Currency)
            .FirstOrDefaultAsync(expression);
            return project!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error finding project :: {ex.Message}");
            return null!;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override Task RollbackTransactionAsync()
    {
        return base.RollbackTransactionAsync();
    }

    public override Task<int> SaveAsync()
    {
        return base.SaveAsync();
    }

    public override string? ToString()
    {
        return base.ToString();
    }

    public override Task<ProjectEntity> UpdateAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity)
    {
        return base.UpdateAsync(expression, updatedEntity);
    }
}
