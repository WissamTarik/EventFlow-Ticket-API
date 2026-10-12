namespace EventTicketingSystem.Domain.Entities.EventEntities
{
    public  class EventCategory:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}