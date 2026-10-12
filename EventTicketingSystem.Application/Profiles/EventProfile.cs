using AutoMapper;
using EventTicketingSystem.Application.Dtos.CommonDtos;
using EventTicketingSystem.Application.Dtos.EventsDtos;
using EventTicketingSystem.Application.Profiles.Resolvers;
using EventTicketingSystem.Domain.Entities.Common;
using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Profiles
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<EventEntity, EventEntityDto>()
                .ForMember(d => d.VenueName, o => o.MapFrom(s => s.Venue.Name))
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<EventImagesResolver>())
                ;
            CreateMap<EventCategory, EventCategoryDto>();
            CreateMap<Venue, VenueDto>();
            CreateMap<Address, AddressDto>();
        }
    }
}
