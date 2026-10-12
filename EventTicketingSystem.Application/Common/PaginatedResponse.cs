using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Common
{
    public sealed record PaginatedResponse<T>
    {
        public PaginatedResponse(int count, int pageSize, int pageIndex,IReadOnlyList<T> data)
        {
            Count = count;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
        }

        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
