using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.OrderDtos;
using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.API.Controllers
{
    public class OrderController(IOrderService orderService):ApiBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(OrderCreateDto dto,CancellationToken ct = default)
        {
            var result = await orderService.CreateOrderAsync(GetCurrentUserEmail(), dto, ct);
            return ToAction(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedResponse<OrderToReturnDto>>> GetAllUserOrders([FromQuery]OrderQueryParams orderQuery,CancellationToken ct = default)
        {
            var result = await orderService.GetAllUserOrdersAsync(GetCurrentUserEmail(), orderQuery, ct);
            return ToAction(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetUserOrderById(Guid id,CancellationToken ct = default)
        {
            var result = await orderService.GetOrderByIdForUserAsync(GetCurrentUserEmail(), id, ct);
            return ToAction(result);
        }
    }
}
