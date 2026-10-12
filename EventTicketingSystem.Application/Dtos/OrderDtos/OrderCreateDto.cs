using EventTicketingSystem.Application.Dtos.CommonDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.OrderDtos
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage ="CartId is required to create order")]
        public string CartId { get; set; } = default!;
        [Required(ErrorMessage ="Billing address is required to create order")]
        public OrderAddressDto BillingAddress { get; set; } = default!;
    }
}
