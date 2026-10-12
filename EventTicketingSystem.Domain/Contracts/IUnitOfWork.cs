using EventTicketingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
