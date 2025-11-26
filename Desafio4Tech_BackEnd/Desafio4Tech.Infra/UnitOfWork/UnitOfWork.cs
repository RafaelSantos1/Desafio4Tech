using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Desafio4Tech.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel);
        }
        public async Task<bool> CommitAsync()
        {
            try
            {
                var committed = 0;
                if (_transaction != null)
                {
                    committed = await _dbContext.SaveChangesAsync();
                    if (committed > 0)
                        _transaction.Commit();
                }
                else
                    committed = await _dbContext.SaveChangesAsync();
                return committed > 0;
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw ex;
            }
        }

        public bool Commit()
        {
            try
            {
                var committed = 0;
                if (_transaction != null)
                {
                    committed = _dbContext.SaveChanges();
                    if (committed > 0)
                        _transaction.Commit();
                }
                else
                    committed = _dbContext.SaveChanges();
                return committed > 0;
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw ex;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false;

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
