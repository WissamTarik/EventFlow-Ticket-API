using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Contracts
{
    public interface IDataSeeder
    {
        public Task SeedDataAsync(CancellationToken ct = default);
    }
}
