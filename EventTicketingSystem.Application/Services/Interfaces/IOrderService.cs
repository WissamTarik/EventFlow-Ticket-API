using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.OrderDtos;
using EventTicketingSystem.Application.QueryParams;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrderAsync(string userEmail,OrderCreateDto dto, CancellationToken ct = default);
        Task<Result<OrderToReturnDto>> GetOrderByIdForUserAsync(string userEmail,Guid orderId, CancellationToken ct = default);
        Task<Result<PaginatedResponse<OrderToReturnDto>>> GetAllUserOrdersAsync(string userEmail, OrderQueryParams queryParams,CancellationToken ct = default);

    }
}
