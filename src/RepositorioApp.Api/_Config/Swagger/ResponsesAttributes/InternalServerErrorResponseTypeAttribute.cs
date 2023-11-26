using System.Net;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class InternalServerErrorResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public InternalServerErrorResponseTypeAttribute() : base((int)HttpStatusCode.InternalServerError, "Internal Server Error", typeof(EnvelopFailResult))
        {
        }
    }
}
