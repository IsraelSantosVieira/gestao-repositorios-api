using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Api.Controllers.Client
{
    [CustomApiExplorerSettings(AppConstants.ClientApiV1Key)]
    [Produces(AppConstants.JsonMediaType)]
    [ApiController]
    public class BaseClientV1Controller : BaseApiController
    {
    }
}
