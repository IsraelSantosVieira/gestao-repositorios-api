using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Utilities.Notifications;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api.Controllers
{
    public class BaseApiController : Controller
    {
        [NonAction]
        protected IActionResult OkResponse()
        {
            return Ok(EnvelopResult.Ok());
        }

        [NonAction]
        protected IActionResult OkResponse<T>(T data = default)
        {
            return Ok(EnvelopDataResult<T>.Ok(data));
        }

        [NonAction]
        protected IActionResult CreatedResponse<T>(T data = default, string url = "")
        {
            return Created(url, EnvelopDataResult<T>.Ok(data));
        }

        [NonAction]
        protected IActionResult NotFoundResponse()
        {
            return new NotFoundObjectResult(EnvelopResult.Fail(new[]
            {
                new Notification(AppMessages.NotFond)
            }));
        }

        [NonAction]
        protected IActionResult UnprocessableEntityResponse()
        {
            return UnprocessableEntity(EnvelopResult.Fail(new[]
            {
                new Notification(AppMessages.UnprocessableEntity)
            }));
        }

        [NonAction]
        protected IActionResult UnauthorizedResponse(ICollection<string> errors = null)
        {
            var notifications = new List<Notification>
            {
                new Notification(AppMessages.Unauthorized)
            };

            errors ??= new List<string>();

            notifications.AddRange(errors.Select(x => new Notification(x)));

            return new ObjectResult(
                EnvelopResult.Fail(notifications))
            {
                StatusCode = 401
            };
        }

        [NonAction]
        protected IActionResult ForbiddenResponse()
        {
            return new ObjectResult(EnvelopResult.Fail(new[]
            {
                new Notification(AppMessages.Forbidden)
            }))
            {
                StatusCode = 403
            };
        }

        public static string GetClientIp(BaseApiController baseController)
        {
            return baseController.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
