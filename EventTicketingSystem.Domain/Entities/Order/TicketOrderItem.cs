using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Order
{
    public class TicketOrderItem:BaseEntity<int>
    {
        public TicketOrderItem()
        {
            
        }
        public TicketOrderItem(EventItemOrdered eventItem, int quantity, decimal unitPrice)
        {
            EventItem = eventItem;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public EventItemOrdered EventItem { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid TicketOrderId { get; set; }
    }
}
