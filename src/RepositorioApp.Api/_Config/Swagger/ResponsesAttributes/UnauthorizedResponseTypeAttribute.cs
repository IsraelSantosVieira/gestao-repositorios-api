using System.Net;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class UnauthorizedResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public UnauthorizedResponseTypeAttribute() : base((int)HttpStatusCode.Unauthorized, "Unauthorized", typeof(EnvelopFailResult))
        {
        }
    }
}
