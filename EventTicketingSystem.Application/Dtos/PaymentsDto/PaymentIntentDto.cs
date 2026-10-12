using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.PaymentsDto
{
    public class PaymentIntentDto(string clientId, string paymentIntentId)
    {
        public string ClientId { get; set; } = clientId;
        public string PaymentIntentId { get; set; } = paymentIntentId;
    }
}
