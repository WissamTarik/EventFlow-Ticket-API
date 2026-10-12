using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Domain.Entities.Order
{
    //part of TicketOrderItem
    public class EventItemOrdered
    {
        public EventItemOrdered()
        {
            
        }
        public EventItemOrdered(int eventId, string eventName, string pictureUrl, DateTimeOffset eventDate)
        {
            EventId = eventId;
            EventName = eventName;
            PictureUrl = pictureUrl;
            EventDate = eventDate;
        }

        public int EventId { get; set; }
        public string EventName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public DateTimeOffset EventDate { get; set; }
    }
}
