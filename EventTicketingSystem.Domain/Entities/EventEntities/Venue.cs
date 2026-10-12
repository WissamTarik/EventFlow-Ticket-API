using EventTicketingSystem.Domain.Entities.Common;

namespace EventTicketingSystem.Domain.Entities.EventEntities
{
    public class Venue:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public Address Location { get; set; } = default!;
    }
}