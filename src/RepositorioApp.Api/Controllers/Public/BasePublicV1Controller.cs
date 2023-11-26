using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Integration;
using RepositorioApp.Domain.AppServices.Integration.Commands;
using RepositorioApp.Domain.Results;
namespace RepositorioApp.Api.Controllers.Public
{
    [CustomApiExplorerSettings(AppConstants.PublicApiV1Key)]
    [Produces(AppConstants.JsonMediaType)]
    [ApiController]
    public class BasePublicV1Controller : BaseApiController
    {
        private readonly IUseIntegrationAppService _useIntegrationAppService;

        public BasePublicV1Controller(
            IUseIntegrationAppService useIntegrationAppService)
        {
            _useIntegrationAppService = useIntegrationAppService;
        }

        protected async Task<IntegrationResult> ValidateIntegration(IntegrationDataCmd command)
        {
            return await _useIntegrationAppService.Validate(command);
        }

        protected bool NotAuthorized(IntegrationResult result)
        {
            return result.Valid == false || result.Error != string.Empty;
        }

        private static IntegrationDataCmd PrepareForIntegration(IntegrationDataCmd command, IntegrationResult result)
        {
            command.IntegrationCode = result.IntegrationCode;
            return command;
        }
    }
}
