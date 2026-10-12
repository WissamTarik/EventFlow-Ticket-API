using AutoMapper;
using EventTicketingSystem.Application.Dtos.EventsDtos;
using EventTicketingSystem.Domain.Entities.EventEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Profiles.Resolvers
{
    public class EventImagesResolver(IConfiguration configuration) : IValueResolver<EventEntity, EventEntityDto, string>
    {
        public string Resolve(EventEntity source, EventEntityDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            return $"{configuration["baseUrl"]}/{source.PictureUrl}";
        }
    }
}
