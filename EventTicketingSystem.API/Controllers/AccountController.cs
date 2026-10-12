using EventTicketingSystem.Application.Dtos.IdentityDtos;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.API.Controllers
{
    public class AccountController(IAuthenticationService service):ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<AccountDto>> LoginAsync(LoginDto dto)
        {
            var result = await service.LoginAsync(dto);
            return ToAction(result);
        }
        [HttpPost("register")]
        public async Task<ActionResult<AccountDto>> RegisterAsync(RegisterDto dto)
        {
            var result = await service.RegisterAsync(dto);
            return ToAction(result);
        }
   
    
        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<UserAddressDto>> GetCurrentUserAddressAsync(CancellationToken ct=default)
        {
            var result = await service.GetCurrentUserAddressAsync(GetCurrentUserEmail(),ct);
            return ToAction(result);
        }
    
        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<UserAddressDto>> UpdateCurrentUserAddressAsync(UserAddressDto dto,CancellationToken ct=default)
        {
            var result = await service.UpdateCurrentUserAddressAsync(GetCurrentUserEmail(),dto,ct);
            return ToAction(result);
        }
   
    
    
    }
}
