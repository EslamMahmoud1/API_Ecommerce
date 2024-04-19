using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Specifications;

namespace Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseProduct<TKey>
    {
        private readonly ApiProjectContext _context;

        public GenericRepository(ApiProjectContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity> specification) => await SpecificationEvaluator<TEntity, TKey>.BuildQuery(_context.Set<TEntity>(), specification).ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return (await _context.Set<TEntity>().FindAsync(id))!;
        }

        public async Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity> specification) => await SpecificationEvaluator<TEntity, TKey>.BuildQuery(_context.Set<TEntity>(), specification).FirstOrDefaultAsync();

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
