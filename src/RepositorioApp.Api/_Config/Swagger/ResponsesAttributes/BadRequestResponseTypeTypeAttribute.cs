using System.Net;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class BadRequestResponseTypeTypeAttribute : BaseResponseTypeAttribute
    {
        public BadRequestResponseTypeTypeAttribute() : base((int)HttpStatusCode.BadRequest, "Bad Request", typeof(EnvelopFailResult))
        {
        }
    }
}
