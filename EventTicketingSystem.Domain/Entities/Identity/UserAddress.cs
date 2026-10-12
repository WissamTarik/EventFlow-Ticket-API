using EventTicketingSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Identity
{
    public class UserAddress:Address
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
    }
}
