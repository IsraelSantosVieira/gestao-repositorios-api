using System;
using System.Net;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class OkResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public OkResponseTypeAttribute(Type type = null) : base((int)HttpStatusCode.OK, "OK", type)
        {
        }
    }
}
