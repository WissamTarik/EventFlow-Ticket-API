
using EventTicketingSystem.Application;
using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Infrastructure;
using Microsoft.Extensions.FileProviders;

namespace EventTicketingSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService(builder.Configuration);
            var myAllowOrigins = "myOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(myAllowOrigins, policy =>
                {
                    policy.WithOrigins("Fronturl")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            var app = builder.Build();
            var scope= app.Services.CreateScope();
           var dataSeeder= scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
           var identitySeeder= scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await  dataSeeder.SeedDataAsync();
            await identitySeeder.SeedDataAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventFlow API v1");
                //    c.RoutePrefix = string.Empty; 
                //});
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"Files"))
            });
            app.UseAuthorization();


            app.MapControllers();

            app.UseCors(myAllowOrigins);
            app.Run();
        }
    }
}
