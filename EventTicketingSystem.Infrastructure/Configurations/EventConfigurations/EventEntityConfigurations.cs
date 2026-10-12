using EventTicketingSystem.Domain.Entities.EventEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Configurations.EventConfigurations
{
    public class EventEntityConfigurations : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Description).HasMaxLength(1000);
            builder.Property(e => e.Price).HasPrecision(18,2);

            builder.HasOne(e => e.Venue)
                   .WithMany()
                   .HasForeignKey(e => e.VenueId);

            builder.HasOne(e => e.Category)
                   .WithMany()
                   .HasForeignKey(e => e.CategoryId);



        }
    }
}
