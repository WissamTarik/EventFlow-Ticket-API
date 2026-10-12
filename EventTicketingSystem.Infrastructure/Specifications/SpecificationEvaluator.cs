using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Specifications
{
    public static  class SpecificationEvaluator
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity,TKey>(IQueryable<TEntity> inputQuery,ISpecification<TEntity,TKey> specs) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specs.Criteria is not null)
                query = query.Where(specs.Criteria);
            if(specs.OrderBy is not null)
            {
                query = query.OrderBy(specs.OrderBy);
            }else if(specs.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specs.OrderByDesc);
            }
            if (specs.isPaginated)
                query = query.Skip(specs.Skip).Take(specs.Take);
            if (specs.IncludeExpression.Count > 0)
                query = specs.IncludeExpression.Aggregate(query, (query, include) => query.Include(include));
            return query;
        }
    }
}
