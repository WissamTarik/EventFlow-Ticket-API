using EventTicketingSystem.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EventTicketingSystem.Infrastructure.Repositories
{
    public class CacheRepository (IConnectionMultiplexer connection): ICacheRepository
    {
        private readonly IDatabase database = connection.GetDatabase();
        public async Task<string?> GetCacheAsync(string key, CancellationToken ct = default)
        {
          var result=   await database.StringGetAsync(key);
            return result;
        }

        public async Task SetCacheAsync(string key, object? value, TimeSpan? Ttl = default)
        {
            await database.StringSetAsync(key, JsonSerializer.Serialize(value), Ttl ?? TimeSpan.FromMinutes(120));
        }
    }
}
