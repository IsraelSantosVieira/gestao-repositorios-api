using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
namespace RepositorioApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            if (Environment.OSVersion.Platform is PlatformID.Unix or PlatformID.MacOSX)
            {
                AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(x => x.AddServerHeader = false)
                .UseStartup<Startup>();
        }
    }
}
