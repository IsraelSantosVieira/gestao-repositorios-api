using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
namespace RepositorioApp.Api._Config.Startup
{
    public static class ResponseHeadersConfig
    {
        public static IApplicationBuilder UseResponseHeadres(this IApplicationBuilder app,
            Dictionary<string, string> toAdd = null,
            Dictionary<string, string> toRemove = null)
        {
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue
                    {
                        Public = true,
                        NoStore = true
                    };

                if (toAdd != null)
                {
                    foreach (var key in toAdd.Keys)
                    {
                        context.Response.Headers.Add(key, toAdd[key]);
                    }
                }

                if (toRemove != null)
                {
                    foreach (var key in toRemove.Keys)
                    {
                        context.Response.Headers.Add(key, toRemove[key]);
                    }
                }

                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Remove("Server");

                await next();
            });

            return app;
        }
    }
}
