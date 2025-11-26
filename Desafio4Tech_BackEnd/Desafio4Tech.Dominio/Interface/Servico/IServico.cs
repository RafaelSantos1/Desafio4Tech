
using System.Linq.Expressions;

namespace Desafio4Tech.Dominio.Interface.Servico
{
    public interface IServico<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(long id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAsync(int page, int pageSize);
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity obj);
        Task DeleteAsync(long id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
