using EventTicketingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> specs,CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(TKey id,CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity,TKey> specs,CancellationToken ct = default);
        Task<int> CountAsync(ISpecification<TEntity,TKey> specs,CancellationToken ct = default);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
