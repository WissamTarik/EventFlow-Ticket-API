using AutoMapper;
using EventTicketingSystem.Application.Dtos.CartDtos;
using EventTicketingSystem.Domain.Entities.CartEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Profiles
{
    public class CartProfile:Profile
    {
        public CartProfile()
        {
            CreateMap<TicketCart, CartDto>().ReverseMap();
            CreateMap<TicketCartItem, CartItemDto>().ReverseMap();
        }
    }
}
