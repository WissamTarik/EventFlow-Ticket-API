using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.EventsDtos
{
    public sealed class EventEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTimeOffset Date { get; set; }
        public int AvailableSeats { get; set; }
        public string VenueName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }
}
