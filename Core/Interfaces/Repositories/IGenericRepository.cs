using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IGenericRepository <TEntity , TKey> where TEntity : BaseProduct<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecification<TEntity> specification);
        Task<TEntity> GetByIdAsync (TKey id);
        Task<TEntity> GetByIdWithSpecificationAsync(ISpecification<TEntity> specification);
        Task AddAsync (TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
