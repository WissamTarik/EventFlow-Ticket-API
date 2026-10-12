using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.CommonDtos
{
    public sealed class AddressDto
    {
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
    }
}
