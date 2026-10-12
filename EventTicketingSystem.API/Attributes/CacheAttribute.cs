using EventTicketingSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace EventTicketingSystem.API.Attributes
{
    public class CacheAttribute(int timeInMinutes):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var key = GetCacheKey(context.HttpContext.Request);
            var cachedData = await cacheService.GetCacheAsync(key);
            if (string.IsNullOrEmpty(cachedData))
            {
                var executed = await next();
                if(executed.Result is OkObjectResult result)
                {
                 await   cacheService.SetCacheAsync(key, result.Value, TimeSpan.FromMinutes(timeInMinutes));

                }
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = cachedData,
                    ContentType = "Application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

        }
        private string GetCacheKey(HttpRequest request)
        {
            StringBuilder key = new();
            key.Append(request.Path);
            foreach (var query in request.Query)
            {
                key.Append($"{query.Key} | {query.Value}");
            }
            return key.ToString();
        }
    }
}
