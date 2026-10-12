namespace EventTicketingSystem.Application.Dtos.OrderDtos
{
    public class OrderItemToReturnDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string PictureUrl { get; set; } = default!;
        public DateTimeOffset EventDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
   
    }
}