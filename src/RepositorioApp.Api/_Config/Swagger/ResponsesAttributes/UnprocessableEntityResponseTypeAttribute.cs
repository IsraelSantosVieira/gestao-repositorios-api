using System.Net;
using RepositorioApp.Utilities.Results;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class UnprocessableEntityResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public UnprocessableEntityResponseTypeAttribute() : base((int)HttpStatusCode.UnprocessableEntity, "Unprocessable Entity", typeof(EnvelopFailResult))
        {
        }
    }
}
