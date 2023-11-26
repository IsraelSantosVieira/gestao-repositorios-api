using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace RepositorioApp.Api._Config.Swagger
{
    public class JsonIgnoreQueryOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.ParameterDescriptions
                .Where(d => d.Source.Id == "Query").ToList()
                .ForEach(param =>
                {
                    var toIgnore = ((DefaultModelMetadata)param.ModelMetadata)
                        .Attributes.PropertyAttributes
                        ?.Any(x => x is JsonIgnoreAttribute);

                    var toRemove = operation.Parameters
                        .SingleOrDefault(p => p.Name == param.Name);

                    if (toIgnore ?? false)
                        operation.Parameters.Remove(toRemove);
                });
        }
    }
}
