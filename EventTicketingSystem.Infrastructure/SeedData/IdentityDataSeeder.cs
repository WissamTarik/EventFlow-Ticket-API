using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.Identity;
using EventTicketingSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure.SeedData
{
    public class IdentityDataSeeder(EventTicketSystemIdentityDbContext context,
                                    UserManager<ApplicationUser> userManager,
                                    RoleManager<IdentityRole> roleManager,
                                    ILogger<IdentityDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync(ct);
            if (pendingMigrations.Any())
              await  context.Database.MigrateAsync(ct);
            try
            {
                if (!context.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
                    await roleManager.CreateAsync(new IdentityRole() { Name = "superAdmin" });
                    logger.LogInformation("Roles is seeded successfully");
                }
                else
                {
                    logger.LogInformation("Roles is already seeded ");

                }
                if (!context.Users.Any())
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "Admin",
                        UserName = "Wesso",
                        Email = "www.wissamtarik2021@gmail.com",
                        PhoneNumber = "01122334455"
                    };
                    var superAdmin = new ApplicationUser()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "AhmedAli",
                        Email = "www.ahmedAli@gmail.com",
                        PhoneNumber = "0111111111"
                    };

                    var adminResult = await userManager.CreateAsync(admin, "P@ssW0rd");
                    if (adminResult.Succeeded)
                        await userManager.AddToRoleAsync(admin, "admin");
                    var superResult = await userManager.CreateAsync(superAdmin, "P@ssW0rd");
                    if (superResult.Succeeded)
                        await userManager.AddToRoleAsync(superAdmin, "superAdmin");
                    logger.LogInformation("Users is seeded successfully");

                }
                else
                {
                    logger.LogInformation("Users is already seeded successfully");

                }



            }
            catch (Exception ex)
            {

                logger.LogError(" Identity Data seeder error", ex.Message);
            }
        }
    }
}
