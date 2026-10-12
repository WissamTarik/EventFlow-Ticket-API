using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Classes
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetCacheAsync(string key)
        => await cacheRepository.GetCacheAsync(key);

        public async Task SetCacheAsync(string key, object? value, TimeSpan? tTl = default)
        {
            await cacheRepository.SetCacheAsync(key, value, tTl);
        }
    }
}
