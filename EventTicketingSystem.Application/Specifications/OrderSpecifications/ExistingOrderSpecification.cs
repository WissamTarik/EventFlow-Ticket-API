using EventTicketingSystem.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventTicketingSystem.Application.Specifications.OrderSpecifications
{
    public class ExistingOrderSpecification(string paymentId) : BaseSpecification<TicketOrder, Guid>(o=>o.PaymentIntentId==paymentId)
    {
    }
}
