using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.QueryParams
{
    public class OrderQueryParams
    {
        public OrderSortValue? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        
    }
    public enum OrderSortValue
    {
        DateAsc,
        DateDesc,
        TotalAsc,
        TotalDesc,
    }
}
