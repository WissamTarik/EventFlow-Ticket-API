using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.IdentityDtos;
using EventTicketingSystem.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Classes
{
    public class AuthenticationService(IIdentityService identityService,ITokenService tokenService) : IAuthenticationService
    {
       

        public async Task<Result<AccountDto>> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            var result = await identityService.FindUserByPasswordAsync(dto.Email, dto.Password);
            if (!result.IsSuccess)
                return Result<AccountDto>.Failure(result.Errors);
            return Result<AccountDto>.Ok(await BuildUserAccount(result.Data));
        }

        public async Task<Result<AccountDto>> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            var existUser = await identityService.FindUserByEmailAddressAsync(dto.Email);
            if (existUser.IsSuccess)
                return Result<AccountDto>.Failure(Error.Conflict("Email.Conflict", "Email is already exist"));

            var result = await identityService.CreateUserAsync(dto);
            if (!result.IsSuccess)
                return Result<AccountDto>.Failure(result.Errors);

            return Result<AccountDto>.Ok(await BuildUserAccount(result.Data));



        }
        public async Task<Result<UserAddressDto>> GetCurrentUserAddressAsync(string email, CancellationToken ct = default)
       => await identityService.GetUserAddressAsync(email, ct);
        public async Task<Result<UserAddressDto>> UpdateCurrentUserAddressAsync(string email, UserAddressDto dto, CancellationToken ct = default)
        => await identityService.UpdateUserAddressAsync(email, dto, ct);

        private  async Task<AccountDto> BuildUserAccount(IdentityUserDto user)
        {
            var roles =await identityService.GetUserRolesAsync(user.Email);
            var token= tokenService.GenerateToken(user.Id, user.UserName, user.Email, roles.Data);
            return new(user.DisplayName, user.Email, token);
                
        }
    }
}
