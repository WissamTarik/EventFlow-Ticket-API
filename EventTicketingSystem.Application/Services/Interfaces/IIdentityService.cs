using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserDto>> FindUserByEmailAddressAsync(string email);
        Task<Result<IdentityUserDto>> FindUserByPasswordAsync(string email,string password);
        Task<Result<IdentityUserDto>> CreateUserAsync(RegisterDto dto);
        Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(string email);
        Task<Result<UserAddressDto>> GetUserAddressAsync(string email,CancellationToken ct=default);
        Task<Result<UserAddressDto>> UpdateUserAddressAsync(string email,UserAddressDto dto,CancellationToken ct=default);
    }
}
