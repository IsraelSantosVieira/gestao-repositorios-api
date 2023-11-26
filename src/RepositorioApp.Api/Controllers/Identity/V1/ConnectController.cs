using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.Api._Config.Swagger.ResponsesAttributes;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Users;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Results;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api.Controllers.Identity.V1
{
    [CustomRoute(AppConstants.IdentityApiV1Key, "connect")]
    public class ConnectController : BaseIdentityV1Controller
    {
        private readonly IAuthenticateUserAppService _appService;

        public ConnectController(IAuthenticateUserAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        ///     Obter access token
        /// </summary>
        /// <returns></returns>
        [OkResponseType(typeof(EnvelopDataResult<AuthenticateUserResult>))]
        [BadRequestResponseTypeType] [InternalServerErrorResponseType]
        [HttpPost("token")]
        public async Task<IActionResult> Auth([FromBody] AuthenticateUserCmd command)
        {
            if (command == null)
                return UnprocessableEntityResponse();

            return await AuthenticateUser(command);
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
