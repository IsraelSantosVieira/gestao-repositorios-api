using System;
namespace RepositorioApp.Api._Config.Swagger
{
    public class SwaggerHeaderAttribute : Attribute
    {
        public SwaggerHeaderAttribute(string headerName, string description = null, bool isRequired = false)
        {
            HeaderName = headerName;
            Description = description;
            IsRequired = isRequired;
        }

        public string HeaderName { get; }
        public string Description { get; }
        public bool IsRequired { get; }
    }
}
