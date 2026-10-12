using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<AccountDto>> LoginAsync(LoginDto dto,CancellationToken ct=default);
        Task<Result<AccountDto>> RegisterAsync(RegisterDto dto,CancellationToken ct=default);
        Task<Result<UserAddressDto>> GetCurrentUserAddressAsync(string email,CancellationToken ct=default);
        Task<Result<UserAddressDto>> UpdateCurrentUserAddressAsync(string email,UserAddressDto dto,CancellationToken ct=default);
    }
}
