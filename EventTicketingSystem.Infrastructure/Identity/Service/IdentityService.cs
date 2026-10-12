using AutoMapper;
using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.IdentityDtos;
using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Identity.Service
{
    public class IdentityService (UserManager<ApplicationUser> userManager,IMapper mapper): IIdentityService
    {
    

        public async Task<Result<IdentityUserDto>> FindUserByEmailAddressAsync(string email)
        {
           var user=await   userManager.FindByEmailAsync(email);
            if (user is null)
                return Result<IdentityUserDto>.Failure(Error.InvalidCredential("Credential.Invalid", "Invalid Credentials"));

            return Result<IdentityUserDto>.Ok(BuildIdentityUser(user));
        }

        public async Task<Result<IdentityUserDto>> FindUserByPasswordAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Result<IdentityUserDto>.Failure(Error.InvalidCredential("InvalidCredentials", "Invalid email or password"));
           var pass=await userManager.CheckPasswordAsync(user, password);
            if(!pass)
                return Result<IdentityUserDto>.Failure(Error.InvalidCredential("InvalidCredentials", "Invalid email or password"));
            return Result<IdentityUserDto>.Ok(BuildIdentityUser(user));

        }
        public async Task<Result<IdentityUserDto>> CreateUserAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                DisplayName = dto.DisplayName,
                UserName = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
           var result=await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserDto>.Failure(errors);
            }
            return Result<IdentityUserDto>.Ok(BuildIdentityUser(user));


        }
      

        public async Task<Result<UserAddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            var user = await GetUserWithAddress(email, ct);
            if (user is null)
                return Result<UserAddressDto>.Failure(Error.NotFound("User.NotFound", $"user with email {email} is not found"));
            if (user.Address is null)
                return Result<UserAddressDto>.Failure(
                    Error.NotFound("UserAddress.NotFound", "This user does not have an address saved."));
            var model = mapper.Map<UserAddressDto>(user.Address);
            return Result<UserAddressDto>.Ok(model);
        }

        public async  Task<Result<UserAddressDto>> UpdateUserAddressAsync(string email, UserAddressDto dto, CancellationToken ct = default)
        {
            var user = await GetUserWithAddress(email, ct);
            if (user is null)
                return Result<UserAddressDto>.Failure(Error.NotFound("User.NotFound", $"user with email {email} is not found"));
            if (user.Address is null)
            {
                user.Address = mapper.Map<UserAddress>(dto);
            }
            else
            {
                user.Address.ZipCode = dto.ZipCode;
                user.Address.City = dto.City;
                user.Address.Country = dto.Country;
                user.Address.Street = dto.Street;
                user.Address.State = dto.State;
            }
            var result=await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error (e.Code, e.Description )).ToList();
                return Result<UserAddressDto>.Failure(errors);

            }
                 return Result<UserAddressDto>.Ok(dto);
              }

        public async Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
          
            var roles= await userManager.GetRolesAsync(user!);
            return Result<IReadOnlyList<string>>.Ok(roles.AsReadOnly());
        }

        private  async Task<ApplicationUser?> GetUserWithAddress(string email, CancellationToken ct = default)
        => await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u => u.Email == email,ct);
        private static IdentityUserDto BuildIdentityUser(ApplicationUser user)
        {
            return new IdentityUserDto(user.Id, user.UserName!, user.DisplayName, user.Email!);
        }


    }
}
