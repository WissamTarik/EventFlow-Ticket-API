using EventTicketingSystem.Domain.Entities.CartEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Contracts
{
    public interface ICartRepository
    {
        Task<TicketCart?> GetCartAsync(string id);
        Task<TicketCart?> CreateOrUpdateAsync(TicketCart ticketCart,TimeSpan? tTL=default);
        Task<bool> RemoveCartAsync(string id);
    }
}
