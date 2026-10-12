using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Order
{
    public class TicketOrder:BaseEntity<Guid>
    {
        public TicketOrder()
        {
            
        }
        public TicketOrder(string userEmail, OrderAddress billingAddress, ICollection<TicketOrderItem> items, decimal subTotal,string paymentIntentId)
        {
            UserEmail = userEmail;
            BillingAddress = billingAddress;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;

        }

        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public TicketOrderStatus Status { get; set; } = TicketOrderStatus.Pending;
        public OrderAddress BillingAddress { get; set; } = default!;//part of it
        public ICollection<TicketOrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal ServiceFees { get; set; }
        public decimal GetTotal() => SubTotal + ServiceFees;
        public string? PaymentIntentId { get; set; }

    }
}
