using EventTicketingSystem.Domain.Entities.EventEntities;
using EventTicketingSystem.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EventTicketingSystem.Infrastructure.Data
{
    public class EventDbContext(DbContextOptions<EventDbContext> options):DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<EventEntity > Events { get; set; }
        public DbSet<EventCategory > EventCategories { get; set; }
        public DbSet<Venue > Venues { get; set; }
        public DbSet<TicketOrder > TicketOrders { get; set; }
        public DbSet<TicketOrderItem > TicketOrderItems { get; set; }
    }
}
