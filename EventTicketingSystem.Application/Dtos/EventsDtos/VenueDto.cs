using EventTicketingSystem.Application.Dtos.CommonDtos;
using EventTicketingSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.EventsDtos
{
    public sealed class VenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public AddressDto Location { get; set; } = default!;
    }
}
