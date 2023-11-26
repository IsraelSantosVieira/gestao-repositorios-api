using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace RepositorioApp.Api._Config.Swagger
{
    public class BasePathOperationFilter : IDocumentFilter
    {
        private readonly string _basePath;

        public BasePathOperationFilter(string basePath)
        {
            _basePath = basePath;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers.Add(new OpenApiServer
            {
                Url = _basePath
            });
        }
    }
}
