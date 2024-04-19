using Core.Interfaces;
using System.Linq.Expressions;

namespace Repository.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Where { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>> OrderBy { get; protected set; }
        public Expression<Func<T, object>> OrderByDesc { get; protected set; }
        public int Skip { get; protected set; }
        public int Take { get; protected set; }
        public bool IsPaginated { get; protected set; }

        public void ApplyPagination(int pageIndex , int pageSize )
        {
            IsPaginated = true;
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }

        //public BaseSpecifications(Expression<Func<T, bool>> whereExpression)
        //{
        //    Where = whereExpression;
        //}
    }
}
