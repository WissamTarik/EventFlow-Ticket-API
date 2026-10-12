using System;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.Application.Dtos.CartDtos
{
    public class CartItemDto
    {
        [Required(ErrorMessage = "Event ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Event ID.")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Event name must be between 2 and 150 characters.")]
        public string EventName { get; set; } = default!;

        [Required(ErrorMessage = "Event picture URL is required.")]
        [Url(ErrorMessage = "Please provide a valid picture URL.")]
        [DataType(DataType.Url)]
        public string PictureUrl { get; set; } = default!;

        [Required(ErrorMessage = "Ticket price is required.")]
        [Range(0.01, 100000.00, ErrorMessage = "Price must be greater than 0.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100 tickets per order.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Event date is required.")]
        public DateTimeOffset EventDate { get; set; }
    }
}