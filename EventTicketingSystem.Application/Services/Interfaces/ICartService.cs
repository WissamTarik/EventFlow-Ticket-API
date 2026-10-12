using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.CartDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<Result<CartDto>> GetCartAsync(string cartId);
        Task<Result<CartDto>> CreateOrUpdateCartAsync(CartDto dto,TimeSpan? tTL=default);
        Task<Result<string>> DeleteCartAsync(string cartId);
    }
}
