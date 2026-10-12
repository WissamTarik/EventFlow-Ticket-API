using EventTicketingSystem.API.Attributes;
using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.EventsDtos;
using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.API.Controllers
{
    public class EventsController(IEventService eventService):ApiBaseController
    {
        [HttpGet]
        [Cache(1)]
        public async Task<ActionResult<PaginatedResponse<EventEntityDto>>> GetAllEvents([FromQuery]EventQueryParams queryParams,CancellationToken ct = default)
        {
          var result=  await eventService.GetAllEventsAsync(queryParams,ct);
            return ToAction(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EventEntityDto>> GetEventById(int id,CancellationToken ct = default)
        {
          var result=  await eventService.GetEventByIdAsync(id,ct);
            return ToAction(result);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<EventCategoryDto>>> GetEventCategories(CancellationToken ct = default)
        {
          var result=  await eventService.GetEventCategoriesAsync(ct);
            return ToAction(result);
        }
        [HttpGet("venues")]
        public async Task<ActionResult<IReadOnlyList<VenueDto>>> GetEventVenuesAsync(CancellationToken ct = default)
        {
          var result=  await eventService.GetAllVenuesAsync(ct);
            return ToAction(result);
        }
    }
}
