// using System.Net;
// using System.Threading.Tasks;
// using RepositorioApp.CrossCutting.Notifications;
// using RepositorioApp.Utilities.Results;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Jae.EShop.Domain._Common.Contracts;
// using Jae.EShop.Domain._Common.Extensions;
// using Jae.EShop.Domain._Common.Messages;
//
// namespace Jae.EShop.Api._Config.ActionFilters
// {
//     public class ValidationFilter : IAsyncActionFilter
//     {
//         private readonly string _commandName;
//
//         public ValidationFilter(string commandName = null)
//         {
//             _commandName = commandName ?? "command";
//         }
//
//         public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//         {
//             if (!context.ActionArguments.ContainsKey(_commandName))
//             {
//                 context.Result = new ObjectResult(EnvelopResult.Fail(new[] {new Notification(AppMessages.UnprocessableEntity)}))
//                 {
//                     StatusCode = (int) HttpStatusCode.UnprocessableEntity
//                 };
//                 return;
//             }
//
//             var commnad = context.ActionArguments[_commandName ?? "command"] as IValidable;
//
//             if (commnad == null)
//             {
//                 context.Result = new ObjectResult(EnvelopResult.Fail(new[] {new Notification(AppMessages.UnprocessableEntity)}))
//                 {
//                     StatusCode = (int) HttpStatusCode.UnprocessableEntity
//                 };
//                 return;
//             }
//
//             IDomainNotification notifications = new DomainNotification();
//
//             if (commnad.Validate(notifications)) await next();
//
//             context.Result = new ObjectResult(EnvelopResult.Fail(notifications.Notify()))
//             {
//                 StatusCode = (int) HttpStatusCode.BadRequest
//             };
//         }
//     }
// }


