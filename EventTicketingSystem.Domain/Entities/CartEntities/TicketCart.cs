using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.CartEntities
{
    public class TicketCart
    {
        public string Id { get; set; } = default!;
        public List<TicketCartItem> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal ServiceFees { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
