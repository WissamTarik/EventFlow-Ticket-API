using AutoMapper;
using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.CartDtos;
using EventTicketingSystem.Application.Dtos.OrderDtos;
using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.CartEntities;
using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Classes
{
    public class CartService(ICartRepository cartRepository,IMapper mapper,IUnitOfWork unitOfWork) : ICartService
    {
        public async Task<Result<CartDto>> CreateOrUpdateCartAsync(CartDto dto,TimeSpan? tTl=default)
        {  

            if(dto is null)
                return Result<CartDto>.Failure(Error.Validation("Cart.NullPayload", "Cart payload cannot be null."));

            var eventRepo = unitOfWork.GetRepository<EventEntity, int>();
            
            foreach (var item in dto.Items)
            {
                var @event = await eventRepo.GetByIdAsync(item.EventId);
                if(@event is not null && @event.AvailableSeats<item.Quantity)
                    return   Result<CartDto>.Failure(Error.Validation("Event.SoldOut",
                 $"Not enough seats available for event '{@event.Name}'. Only {@event.AvailableSeats} seats left."));
            }
            var ticketCart = mapper.Map<TicketCart>(dto);
          var result=await   cartRepository.CreateOrUpdateAsync(ticketCart,tTl);
            if(result is null)
                return Result<CartDto>.Failure(Error.Faliure("Cart.SaveFailed", "Failed to create or update the Cart."));
            //var subtotal = ticketCart.Items.Sum(i => i.Price * i.Quantity);
            //var serviceFees = subtotal * 0.09m;
            //var total = subtotal + serviceFees;
            //ticketCart.Total = total;
            //ticketCart.ServiceFees = serviceFees;
            var model = mapper.Map<CartDto>(result);
          
          //  model.SubTotal = subtotal;
            return Result<CartDto>.Ok(model);

        }

        public async Task<Result<string>> DeleteCartAsync(string cartId)
        {
         var cart=  await  cartRepository.GetCartAsync(cartId);
            if (cart is null)
                return Result<string>.Failure(Error.NotFound("Cart.NotFound", $"Cart with id {cartId} is not found"));
           var result=  await cartRepository.RemoveCartAsync(cartId);
            return result ? Result<string>.Ok("Cart is deleted successfully") : 
                Result<string>.Failure(Error.Faliure("Cart.DeleteFailed", "Failed to delete the cart"));
        }

        public async Task<Result<CartDto>> GetCartAsync(string cartId)
        {
            var cart = await cartRepository.GetCartAsync(cartId);

            if (cart is null)
                return Result<CartDto>.Failure(Error.NotFound("Cart.NotFound", $"Cart with id {cartId} is not found"));
            var model = mapper.Map<CartDto>(cart);
            return Result<CartDto>.Ok(model);
        
        }
    }
}
