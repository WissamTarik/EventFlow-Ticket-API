using EventTicketingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventTicketingSystem.Domain.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
       ICollection<Expression<Func<TEntity,object>>>IncludeExpression { get; set; }
        Expression<Func<TEntity,bool>> Criteria { get; set; }
        Expression<Func<TEntity,object>>? OrderBy { get; set; }
        Expression<Func<TEntity,object>>? OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool isPaginated { get; set; }

    }
}
