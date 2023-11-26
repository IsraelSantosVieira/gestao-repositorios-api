using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.CrossCutting.Contracts;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Notifications;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.FiltersAndAttributes
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException domainException)
            {
                //Logging4NetFactory.GetLogger().Error(domainException.AllNotificationsToText(), domainException);

                context.Result = new ObjectResult(EnvelopResult.Fail(domainException.Notifications))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                return;
            }

            context.HttpContext.RequestServices.GetRequiredService<ILoggerStorageService>()?.Error(context.Exception);

            context.Result = new ObjectResult(EnvelopResult.Fail(new[]
            {
                new Notification(AppMessages.InternalServerError)
            }))
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }


}
