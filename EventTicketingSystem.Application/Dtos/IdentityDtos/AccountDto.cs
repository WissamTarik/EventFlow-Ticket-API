using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.IdentityDtos
{
    public  sealed record AccountDto
    {
        public AccountDto(string displayName, string email, string token)
        {
            DisplayName = displayName;
            Email = email;
            Token = token;
        }

        public string DisplayName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
