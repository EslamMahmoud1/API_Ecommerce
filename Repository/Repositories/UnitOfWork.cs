using Core.Interfaces.Repositories;
using Core.Models;
using Repository.Context;
using System.Collections;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiProjectContext _context;
        private readonly Hashtable Repositories;

        public UnitOfWork(ApiProjectContext context)
        {
            _context = context;
            Repositories = new Hashtable();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseProduct<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (Repositories.ContainsKey(typeName))
                return (Repositories[typeName] as IGenericRepository<TEntity, TKey>)!;
            var repo = new GenericRepository<TEntity,TKey>(_context);
            Repositories.Add(typeName, repo);
            return repo;
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
