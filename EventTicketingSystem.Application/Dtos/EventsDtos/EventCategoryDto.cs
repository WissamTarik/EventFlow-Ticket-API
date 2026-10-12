using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Application.Dtos.EventsDtos
{
    public  class EventCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } =default!;

    }
}
