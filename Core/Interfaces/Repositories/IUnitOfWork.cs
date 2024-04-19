using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity , TKey> GetRepository<TEntity , TKey>() where TEntity : BaseProduct<TKey>;
        Task<int> CompleteAsync();
    }
}
