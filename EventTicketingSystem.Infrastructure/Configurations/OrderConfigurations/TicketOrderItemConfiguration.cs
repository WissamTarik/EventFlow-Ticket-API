using EventTicketingSystem.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Configurations.OrderConfigurations
{
    public class TicketOrderItemConfiguration : IEntityTypeConfiguration<TicketOrderItem>
    {
        public void Configure(EntityTypeBuilder<TicketOrderItem> builder)
        {
            builder.OwnsOne(oi => oi.EventItem, e =>
            {
                e.Property(e => e.PictureUrl).HasColumnName("Event_PictureUrl");
                e.Property(e => e.EventId).HasColumnName("Event_Id");
                e.Property(e => e.EventName).HasColumnName("Event_Name");
                e.Property(e => e.EventDate).HasColumnName("Event_Date");
            });
            builder.Property(e => e.UnitPrice).HasPrecision(18, 2);
        }
    }
}
