using EventTicketingSystem.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Configurations.OrderConfigurations
{
    public class TicketOrderConfiguration : IEntityTypeConfiguration<TicketOrder>
    {
        public void Configure(EntityTypeBuilder<TicketOrder> builder)
        {
            builder.OwnsOne(o => o.BillingAddress, a =>
            {
                a.Property(a => a.ZipCode).HasColumnName("Billing_ZipCode");
                a.Property(a => a.Street).HasColumnName("Billing_Street");
                a.Property(a => a.City).HasColumnName("Billing_City");
                a.Property(a => a.Country).HasColumnName("Billing_Country");
                a.Property(a => a.State).HasColumnName("Billing_State");
            });
            builder.Property(o => o.Status).HasConversion<string>();

            builder.HasMany(o => o.Items)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.ServiceFees).HasPrecision(18, 2);
            builder.Property(o => o.SubTotal).HasPrecision(18, 2);
        }
    }
}
