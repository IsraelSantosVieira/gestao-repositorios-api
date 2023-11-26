using System.Net;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class ForbiddenResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public ForbiddenResponseTypeAttribute() : base((int)HttpStatusCode.Forbidden, "Forbidden", typeof(EnvelopFailResult))
        {
        }
    }
}
