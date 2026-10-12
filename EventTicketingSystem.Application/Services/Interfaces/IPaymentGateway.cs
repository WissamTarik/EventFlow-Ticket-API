using EventTicketingSystem.Application.Dtos.PaymentsDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentIntentDto> CreatePaymentIntentAsync(string currency, decimal amount, CancellationToken ct = default);
        Task<PaymentIntentDto> UpdatePaymentIntentAsync( string paymentIntentId,decimal amount, CancellationToken ct = default);
    }
}
