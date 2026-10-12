using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Contracts
{
    public interface ICacheRepository
    {
       Task SetCacheAsync(string key,object? value, TimeSpan? tTl = default);
        Task<string?> GetCacheAsync(string key, CancellationToken ct = default);
    }
}
