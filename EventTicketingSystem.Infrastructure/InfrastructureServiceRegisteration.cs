using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Services.Interfaces;
using EventTicketingSystem.Domain.Contracts;
using EventTicketingSystem.Domain.Entities.Identity;
using EventTicketingSystem.Infrastructure.Data;
using EventTicketingSystem.Infrastructure.Identity;
using EventTicketingSystem.Infrastructure.Identity.Service;
using EventTicketingSystem.Infrastructure.Payments;
using EventTicketingSystem.Infrastructure.Repositories;
using EventTicketingSystem.Infrastructure.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventTicketingSystem.Infrastructure
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AppConnection"));
            });
            services.AddDbContext<EventTicketSystemIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            }).AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<EventTicketSystemIdentityDbContext>();
            ;
            
            
            
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("redisConnection")!);
            });
            
            services.AddSeedingsDI();
            services.AddRepositories();
            services.AddServices();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddJWTBearer(configuration);
            return services;
        }
        private static IServiceCollection AddSeedingsDI(this IServiceCollection services)
        {
            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            return services;
        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IPaymentGateway, StripePaymentGateway>();
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            return services;
        }
        private static IServiceCollection AddJWTBearer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(nameof(JWTOptions)).Get<JWTOptions>()!;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer =true,
                    ValidateAudience=true,
                    ValidateIssuerSigningKey=true,
                    ValidateLifetime=true,

                    ValidIssuer=jwtOptions.Issuer,
                    ValidAudience=jwtOptions.Audience,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            return services;
        }
      
    }
}
