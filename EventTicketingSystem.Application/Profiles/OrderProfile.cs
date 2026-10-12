using AutoMapper;
using EventTicketingSystem.Application.Dtos.OrderDtos;
using EventTicketingSystem.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddressDto,OrderAddress>().ReverseMap();
            CreateMap<TicketOrderItem, OrderItemToReturnDto>()
                .ForMember(d=>d.EventId,o=>o.MapFrom(s=>s.EventItem.EventId))
                .ForMember(d=>d.EventDate,o=>o.MapFrom(s=>s.EventItem.EventDate))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom(s=>s.EventItem.PictureUrl));

            CreateMap<TicketOrder, OrderToReturnDto>()
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()))
                .ForMember(d => d.OrderItems ,o=> o.MapFrom(s=>s.Items))
                .ForMember(d => d.OrderId ,o=> o.MapFrom(s=>s.Id));
        }
    }
}
