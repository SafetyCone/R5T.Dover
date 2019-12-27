using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;

using R5T.Herulia.Extensions;

using AspNetCoreStartup = Microsoft.AspNetCore.Hosting.IStartup;


namespace R5T.Dover
{
    public static class WebApplicationBuilderHelper
    {
        /// <summary>
        /// Add the web application startup instance as the service instance for <see cref="Microsoft.AspNetCore.Hosting.IStartup"/>.
        /// </summary>
        public static IWebHostBuilder GetDefaultWebHostBuilder(IConfiguration configuration, IWebApplicationStartup webApplicationStartup)
        {
            var webApplicationStartupWrapper = new WebApplicationStartupWrapper(webApplicationStartup);

            var webHostBuilder = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseKestrel()
                .UseDefaultContentRoot()
                .UseIISIntegration()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<AspNetCoreStartup>(webApplicationStartupWrapper);
                })
                ;

            return webHostBuilder;
        }
    }
}
