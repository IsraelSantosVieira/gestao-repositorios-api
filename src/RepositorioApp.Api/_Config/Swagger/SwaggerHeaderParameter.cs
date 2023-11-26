using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace RepositorioApp.Api._Config.Swagger
{
    public class SwaggerHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            var attribute = (SwaggerHeaderAttribute)context.MethodInfo.GetCustomAttribute(typeof(SwaggerHeaderAttribute));

            if (attribute != null)
            {
                var existingParam = operation.Parameters.FirstOrDefault(p =>
                    p.In == ParameterLocation.Header && p.Name == attribute.HeaderName);

                if (existingParam != null) operation.Parameters.Remove(existingParam);

                var operationParameter = new OpenApiParameter
                {
                    Name = attribute.HeaderName,
                    In = ParameterLocation.Header,
                    Description = attribute.Description,
                    Required = attribute.IsRequired,
                    Schema =
                        new OpenApiSchema
                        {
                            Type = "string"
                        }
                };

                if (attribute.HeaderName == "some const")
                {
                    operation.Parameters.Insert(0, operationParameter);
                    return;
                }

                operation.Parameters.Add(operationParameter);
            }
        }
    }
}
