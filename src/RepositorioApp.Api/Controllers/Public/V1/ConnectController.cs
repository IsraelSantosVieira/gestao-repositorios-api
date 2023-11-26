using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.Api._Config.Swagger.ResponsesAttributes;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.CrossCutting.Models;
using RepositorioApp.Domain.AppServices.Integration;
using RepositorioApp.Domain.AppServices.Users;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api.Controllers.Public.V1
{
    [CustomRoute(AppConstants.PublicApiV1Key, "connect")]
    public class ConnectController : BasePublicV1Controller
    {
        private readonly IAuthenticateUserAppService _appService;

        public ConnectController(
            IUseIntegrationAppService useIntegrationAppService,
            IAuthenticateUserAppService appService)
            : base(useIntegrationAppService)
        {
            _appService = appService;
        }

        /// <summary>
        ///     Obter access token para integração com outros sistemas
        /// </summary>
        /// <returns></returns>
        [OkResponseType(typeof(EnvelopDataResult<JwToken>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Integration([FromBody] AuthenticateUserCmd command)
        {
            if (command == null)
                return UnprocessableEntityResponse();

            if (!await _appService.CanAuthenticateSecureUser(command))
            {
                return UnauthorizedResponse();
            }

            var result = await AuthenticateUser(command, true);
            return result;
        }

        private async Task<IActionResult> AuthenticateUser(AuthenticateUserCmd command, bool integrationContext = false)
        {
            if (!string.IsNullOrEmpty(command.Email))
            {
                command.Email = command.Email.ToLower();
            }
            
            if (integrationContext)
            {
                var token = await _appService.IntegrationAuthenticate(command);
                return OkResponse(token);
            }

            var result = await _appService.Authenticate(command);

            if (result.Errors.Any())
            {
                throw new DomainException(result.Errors);
            }

            return OkResponse(result);
        }
    }
}
