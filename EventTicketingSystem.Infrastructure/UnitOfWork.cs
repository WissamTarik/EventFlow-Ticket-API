using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Infrastructure.Data;
using EventTicketingSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure
{
    public class UnitOfWork(EventDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repos = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var entityKey = typeof(TEntity).Name;
            if (_repos.TryGetValue(entityKey, out object entity))
                return (IGenericRepository<TEntity, TKey>)entity;

            var repo = new GenericRepository<TEntity, TKey>(context);
            _repos.Add(entityKey, repo);
            return repo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
       => await context.SaveChangesAsync(ct);
    }
}
