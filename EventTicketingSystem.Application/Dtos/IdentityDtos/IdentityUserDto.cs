using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.IdentityDtos
{
    public sealed record IdentityUserDto
    {
        public IdentityUserDto(string id, string userName, string displayName, string email)
        {
            Id = id;
            UserName = userName;
            DisplayName = displayName;
            Email = email;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
