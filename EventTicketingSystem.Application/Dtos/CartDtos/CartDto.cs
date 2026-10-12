using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventTicketingSystem.Application.Dtos.CartDtos
{
    public class CartDto
    {
        [Required(ErrorMessage = "Cart ID is required.")]
        [StringLength(100, ErrorMessage = "Cart ID cannot exceed 100 characters.")]
        public string Id { get; set; } = default!;

        [Required(ErrorMessage = "Cart items are required.")]
        [MinLength(1, ErrorMessage = "Cart must contain at least one item.")]
        public List<CartItemDto> Items { get; set; } = new();

        [StringLength(100, ErrorMessage = "Payment Intent ID cannot exceed 100 characters.")]
        public string? PaymentIntentId { get; set; }

        [StringLength(150, ErrorMessage = "Client Secret cannot exceed 150 characters.")]
        public string? ClientSecret { get; set; }

        //[Required(ErrorMessage = "Service fee is required.")]
        //[Range(typeof(decimal), "10.00", "1000.00", ErrorMessage = "Service fees must be between 10 and 1000 EGP.")]
        //[DataType(DataType.Currency)]
        public decimal? SubTotal { get; set; }
        public decimal? ServiceFees { get; set; }
        public decimal? Total { get; set; }
    }
}