using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.Api._Config.Swagger.ResponsesAttributes;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Users;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.AppServices.Users.Contracts;
using RepositorioApp.Domain.Results;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api.Controllers.Identity.V1
{
    [CustomRoute(AppConstants.IdentityApiV1Key, "account")]
    public class AccountController : BaseIdentityV1Controller
    {

        [OkResponseType(typeof(EnvelopDataResult<PasswordRecoverResult>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [AllowAnonymous]
        [HttpPost("recover-password")]
        public async Task<IActionResult> RecoverPassword(
            [FromServices] IPasswordRecoverRequestAppService appService,
            [FromBody] RecoverPasswordCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : OkResponse(await appService.Generate(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<PasswordRecoverResult>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [AllowAnonymous]
        [HttpPost("user-activate")]
        public async Task<IActionResult> UserActivate(
            [FromServices] IUserActivateAppService appService,
            [FromBody] UserActivateCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : OkResponse(await appService.Activate(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<ChangeOrResetPasswordResult>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            [FromServices] IResetPasswordAppService appService,
            [FromBody] ResetPasswordCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : OkResponse(await appService.Reset(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<ChangeOrResetPasswordResult>))]
        [BadRequestResponseTypeType] [UnauthorizedResponseType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordAppService appService,
            [FromBody] ChangePasswordCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : OkResponse(await appService.Change(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<UpdateAvatarResult>))]
        [BadRequestResponseTypeType] [UnauthorizedResponseType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpPost("avatar")]
        public async Task<IActionResult> Avatar(
            [FromServices] IUpdateAvatarAppService appService,
            [FromBody] UpdateAvatarCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : OkResponse(await appService.Update(command));
        }

        [OkResponseType(typeof(EnvelopDataResult<UpdateAvatarResult>))]
        [BadRequestResponseTypeType] [UnauthorizedResponseType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpDelete("avatar")]
        public async Task<IActionResult> RemoveAvatar([FromServices] IRemoveAvatarAppService appService)
        {
            return OkResponse(await appService.Remove());
        }

        [OkResponseType(typeof(EnvelopDataResult<NoContentResult>))]
        [BadRequestResponseTypeType] [UnauthorizedResponseType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpDelete("user")]
        public async Task<IActionResult> Remove([FromServices] IRemoveUserService service)
        {
            await service.Remove();
            return NoContent();
        }

        [OkResponseType(typeof(EnvelopDataResult<UserVm>))]
        [BadRequestResponseTypeType] [UnauthorizedResponseType] [InternalServerErrorResponseType]
        [Authorize(AppPolices.Authenticated)]
        [HttpPost("profile")]
        public async Task<IActionResult> Profile(
            [FromServices] IUpdateUserAppService appService,
            [FromBody] UpdateProfileCmd command)
        {
            return command == null
                ? UnprocessableEntityResponse()
                : OkResponse(await appService.Update(command));
        }

        /// <summary>
        ///     Reenvia o código de ativação
        /// </summary>
        /// <param name="appService"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [AllowAnonymous]
        [HttpPost("resend-activation-code")]
        public async Task<IActionResult> ResendActivationCode(
            [FromServices] IManagerUserAppService appService,
            [FromBody] ActivationCodeCmd command)
        {
            if (command == null) return UnprocessableEntityResponse();

            await appService.ResendActivationCode(command);

            return NoContent();
        }

        /// <summary>
        ///     Continua a ativação da conta
        /// </summary>
        /// <param name="appService"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [AllowAnonymous]
        [HttpPost("proceed-activate-code")]
        public async Task<IActionResult> ProceedActivateCode(
            [FromServices] IManagerUserAppService appService,
            [FromBody] ActivationCodeCmd command)
        {
            if (command == null) return UnprocessableEntityResponse();

            await appService.ResendActivationCode(command);

            return NoContent();
        }
    }
}
