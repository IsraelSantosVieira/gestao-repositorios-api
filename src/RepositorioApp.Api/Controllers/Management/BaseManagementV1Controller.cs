using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Api.Controllers.Management
{
    [CustomApiExplorerSettings(AppConstants.ManagementApiV1Key)]
    [Produces(AppConstants.JsonMediaType)]
    [ApiController]
    public class BaseManagementV1Controller : BaseApiController
    {
    }
}
