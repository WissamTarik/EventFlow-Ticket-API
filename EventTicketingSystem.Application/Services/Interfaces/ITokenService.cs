using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string username, string email, IReadOnlyList<string> roles);
    }
}
