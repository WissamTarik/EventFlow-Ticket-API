using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.PaymentsDto;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Payments
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly PaymentIntentService paymentIntentService = new();

        public StripePaymentGateway(IOptions<StripeOptions> options)
        {
            var stripeValues = options.Value;
            StripeConfiguration.ApiKey = stripeValues.SecretKey;
        }
        public async Task<PaymentIntentDto> CreatePaymentIntentAsync(string currency, decimal amount, CancellationToken ct = default)
        {
            PaymentIntentCreateOptions options = new()
            {
                Currency = currency,
                Amount = (long)(amount * 100m),
                AllowedPaymentMethodTypes = ["card"]

            };
            RequestOptions request = new () { IdempotencyKey = Guid.NewGuid().ToString() };
           var result=await  paymentIntentService.CreateAsync(options, request, ct);
            return new(result.ClientSecret, result.Id);
        }

        public async Task<PaymentIntentDto> UpdatePaymentIntentAsync(string paymentIntentId,decimal amount, CancellationToken ct = default)
        {
            PaymentIntentUpdateOptions options = new() { Amount = (long)(amount * 100m) };
            RequestOptions request = new() { IdempotencyKey = Guid.NewGuid().ToString() };
            var result = await paymentIntentService.UpdateAsync(paymentIntentId, options, request, ct);
            return new(result.ClientSecret, result.Id);
        }
    }
}
