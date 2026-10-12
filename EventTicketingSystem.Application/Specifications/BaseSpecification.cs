using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventTicketingSystem.Application.Specifications
{
    public class BaseSpecification<TEntity, TKey>(Expression<Func<TEntity, bool>> criteria) : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; set; } = [];
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = criteria;
        public Expression<Func<TEntity, object>>? OrderBy { get; set ; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set ; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool isPaginated { get ; set ; }

        protected void ApplyInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpression.Add(include);
        }
        protected void ApplyOrderByAsc(Expression<Func<TEntity, object>> expression)
        {
           OrderBy=expression ;
        }
        protected void ApplyOrderByDesc(Expression<Func<TEntity, object>> expression)
         {
           OrderByDesc=expression ;
          }
        protected void ApplyPagination(int pageIndex,int pageSize)
         {
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
            isPaginated = true;
          }

    }
}
