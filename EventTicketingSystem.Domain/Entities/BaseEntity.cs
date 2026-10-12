using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
