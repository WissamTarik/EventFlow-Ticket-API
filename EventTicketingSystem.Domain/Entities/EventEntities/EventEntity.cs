using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.EventEntities
{
    public class EventEntity:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTimeOffset Date { get; set; }
        public int AvailableSeats { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; } = default!;//Location of event
        public int CategoryId { get; set; }
        public EventCategory Category { get; set; } = default!;


    }
}
