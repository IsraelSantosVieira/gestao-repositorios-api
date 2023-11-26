using System.Net;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class NotFoundResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public NotFoundResponseTypeAttribute() : base((int)HttpStatusCode.NotFound, "Not Found", typeof(EnvelopFailResult))
        {
        }
    }
}
