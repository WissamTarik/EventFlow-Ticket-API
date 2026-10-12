using EventTicketingSystem.Application.QueryParams;
using EventTicketingSystem.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventTicketingSystem.Application.Specifications.OrderSpecifications
{
    public class OrderSpecification : BaseSpecification<TicketOrder, Guid>
    {
        public OrderSpecification(string userEmail,OrderQueryParams orderQuery) : base(o=>o.UserEmail.ToLower()==userEmail.ToLower())
        {
            ApplySort(orderQuery.Sort);
            ApplyInclude(o => o.Items);
            ApplyPagination(orderQuery.PageIndex, orderQuery.PageSize);
        }
        public OrderSpecification(string userEmail) : base(o=>o.UserEmail.ToLower()==userEmail.ToLower())
        {
        }
        public OrderSpecification(string userEmail,Guid orderId) : base(o=>o.UserEmail.ToLower()==userEmail.ToLower()&& orderId==o.Id)
        {
            ApplyInclude(o => o.Items);
        }
        private void ApplySort(OrderSortValue? sort)
        {
            switch (sort)
            {
                case OrderSortValue.DateAsc:
                    ApplyOrderByAsc(o => o.OrderDate);
                    break;
                case OrderSortValue.DateDesc:
                    ApplyOrderByDesc(o => o.OrderDate);
                    break;
                case OrderSortValue.TotalAsc:
                    ApplyOrderByAsc(o => o.SubTotal+o.ServiceFees);
                    break;
                case OrderSortValue.TotalDesc:
                    ApplyOrderByDesc(o => o.SubTotal+o.ServiceFees);
                    break;
                default:
                    ApplyOrderByAsc(o => o.OrderDate);
                    break;
            }
        }
    }
}
