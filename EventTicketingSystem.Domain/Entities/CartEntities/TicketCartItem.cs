namespace EventTicketingSystem.Domain.Entities.CartEntities
{
    public class TicketCartItem
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = default!;
        public string PictureUrl  { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public DateTimeOffset EventDate { get; set; }
    }
}