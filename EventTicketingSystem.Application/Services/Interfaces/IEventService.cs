using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.EventsDtos;
using EventTicketingSystem.Application.QueryParams;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<Result<PaginatedResponse<EventEntityDto>>> GetAllEventsAsync(EventQueryParams queryParams,CancellationToken ct = default);
        Task<Result<EventEntityDto>> GetEventByIdAsync(int eventId,CancellationToken ct = default);
        Task<Result<IReadOnlyList<EventCategoryDto>>> GetEventCategoriesAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<VenueDto>>> GetAllVenuesAsync(CancellationToken ct = default);
    }
}
