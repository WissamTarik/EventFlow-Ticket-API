using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.CartEntities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EventTicketingSystem.Infrastructure.Repositories
{
    public class CartRepository(IConnectionMultiplexer connection) : ICartRepository
    {
        private readonly IDatabase database = connection.GetDatabase();
        public async Task<TicketCart?> CreateOrUpdateAsync(TicketCart ticketCart,TimeSpan? tTl=default)
        {
            CalculateTotal(ticketCart);
            var cart = JsonSerializer.Serialize(ticketCart);
            var result=await  database.StringSetAsync(ticketCart.Id, cart, tTl ?? TimeSpan.FromDays(2));
            return result ? ticketCart : null;
        }

        public async Task<TicketCart?> GetCartAsync(string id)
        {
            var data=await database.StringGetAsync(id);
            if (data.IsNullOrEmpty) return null;
            var result =  JsonSerializer.Deserialize<TicketCart>(data.ToString());
            return result??null;
        }

        public async Task<bool> RemoveCartAsync(string id)=>
            await database.KeyDeleteAsync(id);
         private void CalculateTotal(TicketCart ticketCart)
        {
            var subtotal = ticketCart.Items.Sum(i => i.Price * i.Quantity);
            var serviceFees = subtotal * 0.09m;
            var total = subtotal + serviceFees;
            ticketCart.Total = total;
            ticketCart.ServiceFees = serviceFees;
            ticketCart.SubTotal = subtotal;
        }
   
    
    }
}
