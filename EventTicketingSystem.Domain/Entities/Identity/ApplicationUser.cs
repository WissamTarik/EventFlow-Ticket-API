using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public UserAddress Address { get; set; } = default!;
    }
}
