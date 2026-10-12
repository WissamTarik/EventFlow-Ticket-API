using EventTicketingSystem.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventTicketingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController:ControllerBase
    {
        public static ActionResult<T> ToAction<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result);
            return ToProblem(result.Errors);
        }
        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var firstError = errors[0];

            var statusCode = firstError.errorType switch
            {
                ErrorTypes.Forbidden=>StatusCodes.Status403Forbidden,
                ErrorTypes.NotFound=>StatusCodes.Status404NotFound,
                ErrorTypes.Conflict=>StatusCodes.Status409Conflict,
                ErrorTypes.Validation=>StatusCodes.Status400BadRequest,
                ErrorTypes.UnAuthorized=>StatusCodes.Status401Unauthorized,
                ErrorTypes.InvalidCredential=>StatusCodes.Status400BadRequest,
                _=>StatusCodes.Status500InternalServerError
            };
            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = firstError.code,
                Detail = firstError.description,
                Extensions = { ["Errors"] = errors }

            };
            return new ObjectResult(problem) { StatusCode = statusCode };
        }

        protected string GetCurrentUserEmail() => User.FindFirstValue(ClaimTypes.Email)!;
    
    }
}
