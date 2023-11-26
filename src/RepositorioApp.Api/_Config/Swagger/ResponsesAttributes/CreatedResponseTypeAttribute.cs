using System;
using System.Net;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class CreatedResponseTypeAttribute : BaseResponseTypeAttribute
    {
        public CreatedResponseTypeAttribute(Type type = null) : base((int)HttpStatusCode.Created, "Created", type)
        {
        }
    }
}
