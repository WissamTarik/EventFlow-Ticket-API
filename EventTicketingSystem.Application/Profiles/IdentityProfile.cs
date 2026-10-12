using AutoMapper;
using EventTicketingSystem.Application.Dtos.IdentityDtos;
using EventTicketingSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Profiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<UserAddress, UserAddressDto>().ReverseMap();   
        }
    }
}
