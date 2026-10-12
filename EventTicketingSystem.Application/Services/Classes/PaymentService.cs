using AutoMapper;
using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.CartDtos;
using EventTicketingSystem.Application.Dtos.PaymentsDto;
using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Application.Specifications.EventsSpecifiactions;
using EventTicketingSystem.Application.Specifications.OrderSpecifications;
using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.EventEntities;
using EventTicketingSystem.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Classes
{
    public class PaymentService(ICartRepository cartRepository,IUnitOfWork unitOfWork,IPaymentGateway paymentGateway,IMapper mapper) : IPaymentService
    {
        private readonly IGenericRepository<EventEntity, int> eventRepo = unitOfWork.GetRepository<EventEntity, int>();
        private readonly IGenericRepository<TicketOrder, Guid> orderRepo = unitOfWork.GetRepository<TicketOrder, Guid>();
        public async Task<Result<CartDto>> CreateOrUpdatePaymentIntentAsync(string cartId, CancellationToken ct = default)
        {
            var cart = await cartRepository.GetCartAsync(cartId);
            if (cart is null)
                return Result<CartDto>.Failure(Error.NotFound("Cart.NotFound", $"Cart with id {cartId} is not found"));
            var eventSpecs = new EventWithIdsSpecification([.. cart.Items.Select(i => i.EventId)]);
            var allEvents = (await eventRepo.GetAllAsync(eventSpecs, ct)).ToDictionary(e => e.Id);
            foreach (var item in cart.Items)
            {
                if (!allEvents.TryGetValue(item.EventId, out var @event))
                    return Result<CartDto>.Failure(Error.NotFound("Event.NotFound", $"There is no event with id {item.EventId}"));
                if (item.Price != @event.Price)
                    item.Price = @event.Price;

            }
            var subTotal = cart.Items.Sum(i => i.Price * i.Quantity);
            var fees = subTotal * 0.09m;
            var total = subTotal + fees;

            cart.SubTotal = subTotal;
            cart.ServiceFees = fees;
            cart.Total = total;
            PaymentIntentDto paymentIntentDto;
            if(cart.PaymentIntentId is null)
            {
               paymentIntentDto=  await paymentGateway.CreatePaymentIntentAsync("USD", total, ct);
            }
            else
            {
                paymentIntentDto = await paymentGateway.UpdatePaymentIntentAsync(cart.PaymentIntentId, total, ct);
            }
            cart.PaymentIntentId = paymentIntentDto.PaymentIntentId;
            cart.ClientSecret = paymentIntentDto.ClientId;
            var newCart=   await cartRepository.CreateOrUpdateAsync(cart);
            var result = mapper.Map<CartDto>(newCart);
            return Result<CartDto>.Ok(result);
        }

        public async Task MarkPaymentFailedAsync(string paymentIntentId, CancellationToken ct = default)
        {
            var order = await GetOrderByPaymentIntent(paymentIntentId, ct);
            if (order is null)
                return;
            order.Status = TicketOrderStatus.Cancelled;
            orderRepo.Update(order);
          await  unitOfWork.SaveChangesAsync(ct);
        }

        public async Task MarkPaymentSucceededAsync(string paymentIntentId, CancellationToken ct = default)
        {
            var order = await GetOrderByPaymentIntent(paymentIntentId, ct);
            if (order is null)
                return;
               order.Status = TicketOrderStatus.Confirmed;
               orderRepo.Update(order);
            await unitOfWork.SaveChangesAsync(ct);
        }
       private async Task<TicketOrder?> GetOrderByPaymentIntent(string paymentIntentId,CancellationToken ct = default)
        {
            var orderSpecs = new ExistingOrderSpecification(paymentIntentId);
            var order = await orderRepo.GetByIdAsync(orderSpecs, ct);
            if (order is null)
                return null;
            return order;
        }
    
    }
}
