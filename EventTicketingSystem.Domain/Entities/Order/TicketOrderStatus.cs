using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Order
{
    public enum TicketOrderStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Refunded
    }
}
