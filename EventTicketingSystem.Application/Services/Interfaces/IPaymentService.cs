using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.CartDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Result<CartDto>> CreateOrUpdatePaymentIntentAsync(string cartId, CancellationToken ct = default);
        Task MarkPaymentSucceededAsync(string paymentIntentId, CancellationToken ct = default);
        Task MarkPaymentFailedAsync(string paymentIntentId, CancellationToken ct = default);
    }
}
