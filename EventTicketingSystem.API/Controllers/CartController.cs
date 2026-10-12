using EventTicketingSystem.Application.Dtos.CartDtos;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.API.Controllers
{
    public class CartController(ICartService cartService):ApiBaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCartAsync(string id)
        {
          var result=  await cartService.GetCartAsync(id);
            return ToAction(result);
        }
        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateOrUpdateCartAsync(CartDto dto)
        {
          var result=  await cartService.CreateOrUpdateCartAsync(dto);
            return ToAction(result);
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteCartAsync(string id)
        {
          var result=  await cartService.DeleteCartAsync(id);
            return ToAction(result);
        }
    }
}
