using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;

using R5T.Richmond;
using R5T.Herulia.Extensions;

using AspNetCoreStartup = Microsoft.AspNetCore.Hosting.IStartup;


namespace R5T.Dover
{
    public static class WebApplicationBuilder
    {
        public static IWebHostBuilder UseStartup<TStartup>()
            where TStartup : class, IWebApplicationStartup
        {
            var webHostBuilder = WebApplicationBuilder.UseStartup<TStartup, DefaultApplicationConfigurationStartup>();
            return webHostBuilder;
        }

        public static IWebHostBuilder UseStartup<TStartup, TConfigurationStartup>()
            where TStartup : class, IWebApplicationStartup
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            // Build the standard startup.
            var webApplicationStartup = ApplicationBuilderInternals.GetApplicationStartup<TStartup>();

            // Configuration.
            var webApplicationConfigurationBuilder = new ConfigurationBuilder();

            // Get the configuration service provider to use during configuration of the application's configuration builder.
            var configurationServiceProvider = ApplicationBuilderInternals.UseConfigurationStartup<TConfigurationStartup>();

            webApplicationStartup.ConfigureConfiguration(webApplicationConfigurationBuilder, configurationServiceProvider);

            var webApplicationConfiguration = webApplicationConfigurationBuilder.Build();

            // Configure services, Configure service instances, and Configure(IApplicationBuilder).
            var webHostBuilder = WebApplicationBuilder.GetDefaultWebHostBuilder(webApplicationConfiguration, webApplicationStartup);
            return webHostBuilder;
        }

        /// <summary>
        /// Add the web application startup instance as the service instance for <see cref="Microsoft.AspNetCore.Hosting.IStartup"/>.
        /// </summary>
        private static IWebHostBuilder GetDefaultWebHostBuilder(IConfiguration configuration, IWebApplicationStartup webApplicationStartup)
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
