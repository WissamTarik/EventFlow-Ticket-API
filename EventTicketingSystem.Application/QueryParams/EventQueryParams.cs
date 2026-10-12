using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.QueryParams
{
    public sealed record EventQueryParams
    {
        public int? CategoryId { get; set; }
        public int ? VenueId { get; set; }
        public string? Search { get; set; }
        public EventSortValue? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
    public enum EventSortValue
    {
        DateAsc,
        DateDesc,
        PriceAsc,
        PriceDesc,
        SeatAsc,
        SeatDesc,
    }
}
