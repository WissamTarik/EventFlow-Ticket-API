using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Entities.EventEntities;
using EventTicketingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EventTicketingSystem.Infrastructure.SeedData
{
    public class CatalogDataSeeder(EventDbContext context,ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await context.Database.MigrateAsync(ct);

                var filePath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedIfEmpty<int, Venue>(filePath, "venues.json", ct);
                await SeedIfEmpty<int, EventCategory>(filePath, "categories.json", ct);
                await SeedIfEmpty<int, EventEntity>(filePath, "events.json", ct);
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }



        }
        private async Task SeedIfEmpty<TKey,TEntity>(string filePath,string fileName,CancellationToken ct) where TEntity:BaseEntity<TKey>
        {
            if(await context.Set<TEntity>().AnyAsync(ct))
            {
                logger.LogInformation($"{typeof(TEntity).Name} table is already seeded");
                return;
            }
            var file = Path.Combine(filePath, fileName);
            if (!File.Exists(file))
            {
                logger.LogError($"File is not exist for {typeof(TEntity).Name}");

            }
            using var fileStream = File.OpenRead(file);

            var data =await JsonSerializer.DeserializeAsync<IReadOnlyList<TEntity>>(fileStream,JsonSerializerOptions.Web,ct);
            if (data is not null && data.Count > 0)
            {
                await context.AddRangeAsync(data);
                var count = await context.SaveChangesAsync(ct);
                logger.LogInformation($"{count} rows added to  {typeof(TEntity).Name} Table");

            }
            else
            {
                logger.LogError($"Failed to seed data to {typeof(TEntity).Name} Table");
            }


               
        }
    }
}
