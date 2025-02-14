using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class, IBaseRepository<TEntity>
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null)
            return false;

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} :: {ex.Message}");
            return false;
        }
    }

    public Task<IEnumerable<TEntity>> GetAsync()
    {
        try
        {

        }
        catch (Exception ex)
        {
            
        }
    }

    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    public Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    public Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

}
