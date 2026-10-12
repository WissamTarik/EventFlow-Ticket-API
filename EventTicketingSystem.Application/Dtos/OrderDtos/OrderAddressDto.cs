using EventTicketingSystem.Application.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.OrderDtos
{
    public class OrderAddressDto:UserAddressDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
