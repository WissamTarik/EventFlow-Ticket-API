using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventTicketingSystem.Application.Specifications.EventsSpecifiactions
{
    public class EventSpecification : BaseSpecification<EventEntity, int>
    {
        public EventSpecification(EventQueryParams queryParams) : base(e=>
            (!queryParams.CategoryId.HasValue||e.CategoryId==queryParams.CategoryId)&&
            (!queryParams.VenueId.HasValue||e.VenueId==queryParams.VenueId)&&
            (string.IsNullOrEmpty(queryParams.Search)||e.Name.ToLower().Contains(queryParams.Search.ToLower()))
            )
        {
            ApplySort(queryParams.Sort);
            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
            AddInclude();

        }
        public EventSpecification(int eventId) : base(e=>e.Id==eventId)
        {
            AddInclude();
        }
        private void ApplySort(EventSortValue? sortValue)
        {
            switch (sortValue)
            {
                    case EventSortValue.DateAsc:
                    ApplyOrderByAsc(e => e.Date);
                        break;
                    case EventSortValue.DateDesc:
                    ApplyOrderByDesc(e => e.Date);

                    break;
                    case EventSortValue.PriceAsc:
                    ApplyOrderByAsc(e => e.Price);

                    break;
                    case EventSortValue.PriceDesc:
                    ApplyOrderByDesc(e => e.Price);

                    break;
                    case EventSortValue.SeatAsc:
                    ApplyOrderByAsc(e => e.AvailableSeats);

                    break;
                    case EventSortValue.SeatDesc:
                    ApplyOrderByDesc(e => e.AvailableSeats);

                    break;
                    default:
                    ApplyOrderByAsc(e => e.Date);

                    break;
            }

        }
        private void AddInclude()
        {
            ApplyInclude(e => e.Venue);
            ApplyInclude(e => e.Category);
        }
    }
}
