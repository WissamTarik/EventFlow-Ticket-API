using EventTicketingSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Order
{
   //part of table(owned)
    public class OrderAddress:Address
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
