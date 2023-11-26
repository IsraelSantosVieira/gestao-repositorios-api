using System;
using Swashbuckle.AspNetCore.Annotations;
namespace RepositorioApp.Api._Config.Swagger.ResponsesAttributes
{
    public class BaseResponseTypeAttribute : SwaggerResponseAttribute
    {
        public BaseResponseTypeAttribute(int statusCode, string description = null, Type type = null) : base(statusCode, description, type)
        {
        }
    }
}
