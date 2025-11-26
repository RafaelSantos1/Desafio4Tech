using System.Linq.Expressions;

namespace Desafio4Tech.Dominio.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllAsync(int page, int pageSize);
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity obj);
        Task<TEntity> AddAsync(TEntity obj);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAsync(TEntity obj);
        Task DeleteAsync(long id);
        Task DeleteAllAsync();
    }
}
