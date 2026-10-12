using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Infrastructure.Data;
using EventTicketingSystem.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey>(EventDbContext context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.Set<TEntity>().ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
        => await context.Set<TEntity>().FindAsync(id,ct);
        public void Add(TEntity entity)
        {
            context.Add(entity);
        }
        public void Update(TEntity entity)
        {
            context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default)
        => await ApplySpecs(specs).ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default)
        => await ApplySpecs(specs).FirstOrDefaultAsync(ct);

        private IQueryable<TEntity> ApplySpecs(ISpecification<TEntity,TKey> specs)
        =>SpecificationEvaluator.ApplySpecification<TEntity, TKey>(context.Set<TEntity>(), specs);

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specs, CancellationToken ct = default)
       => await ApplySpecs(specs).CountAsync(ct);

    }
}
