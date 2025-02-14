using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression); 
    }
}