using EventTicketingSystem.Application.Common;
using EventTicketingSystem.Application.Services.Classes;
using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EventTicketingSystem.Application
{
    public static class ApplicationServiceRegestaration
    {
       public static IServiceCollection AddApplicationService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddAutoMapper(config => { }, typeof(ApplicationServiceRegestaration).Assembly);
            services.AddOptionsService(configuration);
            return services;
        }
        private static IServiceCollection AddOptionsService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
            services.Configure<StripeOptions>(configuration.GetSection("StripeOptions"));
            return services;
        }
    }
}
