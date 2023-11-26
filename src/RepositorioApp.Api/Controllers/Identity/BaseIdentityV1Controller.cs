using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Api._Config.FiltersAndAttributes;
using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Api.Controllers.Identity
{
    [CustomApiExplorerSettings(AppConstants.IdentityApiV1Key)]
    [Produces(AppConstants.JsonMediaType)]
    [ApiController]
    public class BaseIdentityV1Controller : BaseApiController
    {
    }
}
