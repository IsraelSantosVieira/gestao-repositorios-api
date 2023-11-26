using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RepositorioApp.Api._Config.Swagger;
using RepositorioApp.CrossCutting.Config;
using RepositorioApp.CrossCutting.Constants;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace RepositorioApp.Api._Config.Startup
{
    public static class ApiDocsConfig
    {
        public static IServiceCollection AppAddApiDocs(this IServiceCollection services, IConfiguration config)
        {
            var appConfig = new AppConfig();

            config.GetSection(nameof(AppConfig)).Bind(appConfig);

            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                foreach (var item in AppConstants.ApisConfiguration.Values.Where(x => x.ExposeOnSwagger))
                {
                    options.SwaggerDoc(item.Group,
                        new OpenApiInfo
                        {
                            Title = item.Title,
                            Version = item.Version,
                            Description = item.Description
                        });
                }

                var prefixAssembly = "RepositorioApp";

                new[]
                    {
                        "Api", "Domain", "CrossCutting"
                    }
                    .Select(x => Path.Combine(AppContext.BaseDirectory, $"{prefixAssembly}.{x}.xml"))
                    .ToList()
                    .ForEach(path => options.IncludeXmlComments(path));

                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Name = "Authorization",
                        Description = "",
                        In = ParameterLocation.Header,
                        BearerFormat = "{access_token}",
                        Scheme = "Bearer"
                    });

                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.OperationFilter<JsonIgnoreQueryOperationFilter>();

                options.OperationFilter<SwaggerHeaderParameter>();

                options.DocumentFilter<BasePathOperationFilter>(config.GetSection("AppConfig:BasePath").Value);

                options.SchemaFilter<EnvelopFailResultExampleSchemaFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static IApplicationBuilder AppUseApiDocs(this IApplicationBuilder app, IConfiguration config)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = false;
                c.RouteTemplate = "docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.InjectStylesheet($"{config.GetSection("AppConfig:BaseUrl").Value}/swagger-ui/custom-swagger-ui.css");

                foreach (var item in AppConstants.ApisConfiguration.Values.Where(x => x.ExposeOnSwagger))
                {
                    options.SwaggerEndpoint(item.SwaggerEndpoint, item.Title);
                }

                options.RoutePrefix = "docs";

                options.DefaultModelRendering(ModelRendering.Example);
                options.DefaultModelsExpandDepth(-1);
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.List);
                options.EnableDeepLinking();
                options.ShowExtensions();

                options.SupportedSubmitMethods(
                    SubmitMethod.Options,
                    SubmitMethod.Get,
                    SubmitMethod.Post,
                    SubmitMethod.Put,
                    SubmitMethod.Patch,
                    SubmitMethod.Delete);
            });

            return app;
        }
    }
}
