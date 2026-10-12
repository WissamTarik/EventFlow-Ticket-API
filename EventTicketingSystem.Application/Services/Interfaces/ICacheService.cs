using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, object? value, TimeSpan? tTl = default);
        Task<string?> GetCacheAsync(string key);
    }
}
