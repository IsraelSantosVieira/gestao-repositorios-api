using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RepositorioApp.Utilities.Notifications;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.FiltersAndAttributes
{
    public class CommandValidationAttribute : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                return next();
            }

            context.Result = new ObjectResult(EnvelopResult.Fail(
                context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => new Notification(x.ErrorMessage))
                    .ToArray()))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            return Task.CompletedTask;
        }
    }
}
