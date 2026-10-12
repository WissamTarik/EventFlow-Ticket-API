using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Specifications.EventsSpecifiactions
{
    public class EventCountSpecification:BaseSpecification<EventEntity,int>
    {
        public EventCountSpecification(EventQueryParams queryParams) : base(e =>
        (!queryParams.CategoryId.HasValue || e.CategoryId == queryParams.CategoryId) &&
        (!queryParams.VenueId.HasValue || e.VenueId == queryParams.VenueId) &&
        (string.IsNullOrEmpty(queryParams.Search) || e.Name.ToLower().Contains(queryParams.Search.ToLower()))
        )
        {

        }
    }
}
