using EventTicketingSystem.Domain.Entities.EventEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Configurations.EventConfigurations
{
    public class VenueConfigurations : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.OwnsOne(e => e.Location, l =>
            {
                l.Property(l => l.Street).HasColumnName("Venue_Street");
                l.Property(l => l.City).HasColumnName("Venue_City");
                l.Property(l => l.ZipCode).HasColumnName("Venue_ZipCode");
                l.Property(l => l.Country).HasColumnName("Venue_Country");
                l.Property(l => l.State).HasColumnName("Venue_State");
            });
        }
    }
}
