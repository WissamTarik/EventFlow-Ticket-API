using EventTicketingSystem.Application.Dtos.CommonDtos;
using EventTicketingSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid OrderId { get; set; }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } = default!;
        public OrderAddressDto BillingAddress { get; set; } = default!;
        public ICollection<OrderItemToReturnDto> OrderItems { get; set; } = [];
        public decimal Subtotal { get; set; }
        public decimal ServiceFees { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntentId { get; set; }

    }
}
