using System.Data;

namespace Desafio4Tech.Dominio.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task<bool> CommitAsync();
    }
}
