using AutoMapper;
using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Interface.Servico;
using System.Linq.Expressions;

namespace Desafio4Tech.Servico.Servico
{
    public class Servico<TEntity> : IServico<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public Servico(IRepository<TEntity> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            var item = await _repository.AddAsync(obj);
            await _uow.CommitAsync();
            return item;
        }
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _uow.CommitAsync();
        }

        public virtual async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.GetAsync(expression);
        }

        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.SingleAsync(expression);
        }

        public virtual async Task<IQueryable<TEntity>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IQueryable<TEntity>> GetAsync(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity obj)
        {
            var item = await _repository.UpdateAsync(obj);
            await _uow.CommitAsync();
            return item;
        }

        public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity obj)
        {
            var item = await _repository.UpdateAsync(predicate, obj);
            await _uow.CommitAsync();
            return item;
        }
    }
}
