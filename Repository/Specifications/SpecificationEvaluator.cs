using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Specifications
{
    public class SpecificationEvaluator <TEntity , TKey> where TEntity : BaseProduct<TKey>
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if(specification.Where is not null)
                query = query.Where(specification.Where);

            query = specification.Includes
                .Aggregate(query,(currentQuery,expression) => currentQuery.Include(expression));

            if(specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDesc is not null)
                query = query.OrderByDescending(specification.OrderByDesc);
            return query;
        }
    }
}
