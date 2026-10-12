using AutoMapper;
using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Dtos.EventsDtos;
using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Application.Specifications;
using EventTicketingSystem.Application.Specifications.EventsSpecifiactions;
using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Services.Classes
{
    public class EventService(IUnitOfWork unitOfWork,IMapper mapper) : IEventService
    {
        private readonly IGenericRepository<EventEntity,int> _EventRepo = unitOfWork.GetRepository<EventEntity, int>();
        private readonly IGenericRepository<EventCategory,int> _CategoryRepo = unitOfWork.GetRepository<EventCategory, int>();
        private readonly IGenericRepository<Venue,int> _VenueRepo = unitOfWork.GetRepository<Venue, int>();
        public async Task<Result<PaginatedResponse<EventEntityDto>>> GetAllEventsAsync(EventQueryParams queryParams,CancellationToken ct = default)
        {

            var eventSpecs = new EventSpecification(queryParams);
            var result =await _EventRepo.GetAllAsync(eventSpecs,ct);
            var dto = mapper.Map<IReadOnlyList<EventEntityDto>>(result);
            var countSpecs = new EventCountSpecification(queryParams);
            var count =await  _EventRepo.CountAsync(countSpecs, ct);
            var response = new PaginatedResponse<EventEntityDto>(count, queryParams.PageSize, queryParams.PageIndex, dto);
            return Result<PaginatedResponse<EventEntityDto>>.Ok(response);
        }

        public async Task<Result<IReadOnlyList<VenueDto>>> GetAllVenuesAsync(CancellationToken ct = default)
        {
            var result = await _VenueRepo.GetAllAsync(ct);
            var dto = mapper.Map<IReadOnlyList<VenueDto>>(result);
            return Result<IReadOnlyList<VenueDto>>.Ok(dto);
        }


        public async Task<Result<IReadOnlyList<EventCategoryDto>>> GetEventCategoriesAsync(CancellationToken ct = default)
        {
            var result = await _CategoryRepo.GetAllAsync(ct);
            var dto = mapper.Map<IReadOnlyList<EventCategoryDto>>(result);
            return Result<IReadOnlyList<EventCategoryDto>>.Ok(dto);
        }
        public async Task<Result<EventEntityDto>> GetEventByIdAsync(int eventId, CancellationToken ct = default)
        {
            var eventSpecs = new EventSpecification(eventId);

            var result = await _EventRepo.GetByIdAsync(eventSpecs, ct);
            if (result is null)
                return Result<EventEntityDto>.Failure(Error.NotFound("Event.NotFound",$"Event with id {eventId} is not found"));
            var dto = mapper.Map<EventEntityDto>(result);
            return Result<EventEntityDto>.Ok(dto);
        }

    }
}
