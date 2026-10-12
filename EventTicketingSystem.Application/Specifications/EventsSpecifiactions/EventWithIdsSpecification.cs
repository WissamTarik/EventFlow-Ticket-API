using EventTicketingSystem.Domain.Entities.EventEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EventTicketingSystem.Application.Specifications.EventsSpecifiactions
{
    public class EventWithIdsSpecification : BaseSpecification<EventEntity, int>
    {
        public EventWithIdsSpecification(HashSet<int> eventIds) : base(e=>eventIds.Contains(e.Id))
        {
        }
    }
}
