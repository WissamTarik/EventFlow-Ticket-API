using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.CartDtos;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace EventTicketingSystem.API.Controllers
{
    public class PaymentController(IPaymentService paymentService,
                                   IOptions<StripeOptions> options,
                                   ILogger<PaymentController> logger):ApiBaseController
    {
        [HttpPost("{cartId}")]
        [Authorize]
        public async Task<ActionResult<CartDto>> CreateOrUpdatePaymentIntentAsync(string cartId,CancellationToken ct = default)
        {
            var result = await paymentService.CreateOrUpdatePaymentIntentAsync(cartId, ct);
            return ToAction(result);
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook(CancellationToken ct=default)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(ct);
            try
            {
                var stripeSignature = Request.Headers["Stripe-Signature"];
                var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, options.Value.Webhook);
                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                      await  paymentService.MarkPaymentSucceededAsync(paymentIntent!.Id, ct);
                        break;
                    case EventTypes.PaymentIntentPaymentFailed:
                        var paymentIntentFailed = stripeEvent.Data.Object as PaymentIntent;
                        await paymentService.MarkPaymentFailedAsync(paymentIntentFailed!.Id, ct);

                        break;
                  
                }
                return Ok();
            }
            catch (Exception ex)
            {

                logger.Log(LogLevel.Error, ex.Message);
                return BadRequest();
            }
        }
    }
}
