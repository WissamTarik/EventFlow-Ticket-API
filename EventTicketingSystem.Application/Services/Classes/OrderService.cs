using AutoMapper;
using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.OrderDtos;
using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Application.Specifications.EventsSpecifiactions;
using EventTicketingSystem.Application.Specifications.OrderSpecifications;
using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.EventEntities;
using EventTicketingSystem.Domain.Entities.Order;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Classes
{
    public class OrderService(IUnitOfWork unitOfWork,ICartRepository cartRepository,IMapper mapper) : IOrderService
    {
        private readonly IGenericRepository<EventEntity, int> eventRep = unitOfWork.GetRepository<EventEntity, int>();
        private readonly IGenericRepository<TicketOrder, Guid> orderRep = unitOfWork.GetRepository<TicketOrder, Guid>();

        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(string userEmail,OrderCreateDto dto, CancellationToken ct = default)
        {
            var cart = await cartRepository.GetCartAsync(dto.CartId);
            if (cart is null)
                return Result<OrderToReturnDto>.Failure(Error.NotFound("Cart.NotFound", $"No cart with id {dto.CartId}"));
            if (cart.Items is null ||cart.Items.Count == 0)
                return Result<OrderToReturnDto>.Failure(Error.Validation("CartItems.Empty", "No items in the cart"));
            if(cart.PaymentIntentId is null)
                return Result<OrderToReturnDto>.Failure(Error.Validation("PaymentIntent.Invalid", $"'Payment Intent' is null."));

            var orderList = new List<TicketOrderItem>();
            var eventIdsSpecs = new EventWithIdsSpecification([.. cart.Items.Select(e => e.EventId)]);

            var allEvents = (await eventRep.GetAllAsync(eventIdsSpecs, ct)).ToDictionary(e => e.Id);

            foreach (var item in cart.Items)
            {
                if (!allEvents.TryGetValue(item.EventId, out EventEntity @event))
                    return Result<OrderToReturnDto>.Failure(Error.NotFound("Event.NotFound", $"Event with id {item.EventId} is not found"));

                if (@event.Price != item.Price) item.Price = @event.Price;

                if (@event.AvailableSeats < item.Quantity)
                    return Result<OrderToReturnDto>.Failure(Error.Validation("Event.SoldOut",
            $"Not enough seats available for event '{@event.Name}'. Only {@event.AvailableSeats} seats left."));

                @event.AvailableSeats -= item.Quantity;
                eventRep.Update(@event);
                
                var eventItem = new EventItemOrdered(item.EventId, item.EventName, item.PictureUrl, item.EventDate);

                var ticketOrderItem = new TicketOrderItem(eventItem, item.Quantity, item.Price);
                orderList.Add(ticketOrderItem);

                
            }
            var subtotal = orderList.Sum(i => i.UnitPrice * i.Quantity);
            var serviceFees = subtotal * 0.09m;
            var existingOrderSpecs = new ExistingOrderSpecification(cart.PaymentIntentId);
            var existingOrder = await orderRep.GetByIdAsync(existingOrderSpecs, ct);
            if (existingOrder is not null)
                orderRep.Delete(existingOrder);
            var order = new TicketOrder(userEmail, mapper.Map<OrderAddress>(dto.BillingAddress), orderList, subtotal, cart.PaymentIntentId)
            {
                ServiceFees = serviceFees
            };
            orderRep.Add(order);
            var count =await unitOfWork.SaveChangesAsync(ct);
            if (count == 0)
             return   Result<OrderToReturnDto>.Failure(Error.Faliure("Order.Failure", "Failed to create order"));

            var result = mapper.Map<OrderToReturnDto>(order);
            return Result<OrderToReturnDto>.Ok(result);
        }
        
        public async Task<Result<PaginatedResponse<OrderToReturnDto>>> GetAllUserOrdersAsync(string userEmail,OrderQueryParams orderQuery, CancellationToken ct = default)
        {
            var orderSpecs = new OrderSpecification(userEmail,orderQuery);
            var orders = await orderRep.GetAllAsync(orderSpecs,ct);
            if (orders is null||!orders.Any())
                return Result<PaginatedResponse<OrderToReturnDto>>.Failure(Error.NotFound("Orders.NotFound", $"user with email {userEmail} has not orders yet"));
            var orderCountSpecs = new OrderSpecification(userEmail);
            var ordersCount = await orderRep.CountAsync(orderCountSpecs,ct);
            var result = mapper.Map< IReadOnlyList<OrderToReturnDto>>(orders);
            PaginatedResponse<OrderToReturnDto> pagination = new(ordersCount, orderQuery.PageSize, orderQuery.PageIndex, result);
            return Result<PaginatedResponse<OrderToReturnDto>>.Ok(pagination);


        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdForUserAsync(string userEmail, Guid orderId, CancellationToken ct = default)
        {
            var orderSpecs = new OrderSpecification(userEmail, orderId);
            var order = await orderRep.GetByIdAsync(orderSpecs, ct);
            if (order is null )
                return Result<OrderToReturnDto>.Failure(Error.NotFound("Order.NotFound", $"Invalid order id"));
            var result = mapper.Map<OrderToReturnDto>(order);
            return Result<OrderToReturnDto>.Ok(result);

        }
    }
}
