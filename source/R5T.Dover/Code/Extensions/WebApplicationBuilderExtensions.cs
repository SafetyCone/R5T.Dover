using System;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;

using R5T.Derby;
using R5T.Richmond;


namespace R5T.Dover
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// The provided <paramref name="configurationServiceProvider"/> provides services during the configuration configuration process.
        /// </summary>
        public static IWebHostBuilder UseStartup<TStartup>(this WebApplicationBuilder webApplicationBuilder, IServiceProvider configurationServiceProvider)
            where TStartup: class, IWebApplicationStartup
        {
            // Build the standard startup.
            var webApplicationStartup = ApplicationBuilderHelper.GetStartupInstance<TStartup>();

            // Configuration.
            var applicationConfigurationBuilder = new ConfigurationBuilder();

            webApplicationStartup.ConfigureConfiguration(applicationConfigurationBuilder, configurationServiceProvider);

            var webApplicationConfiguration = applicationConfigurationBuilder.Build();

            // Configure services, Configure service instances, and Configure(IApplicationBuilder).
            var webHostBuilder = WebApplicationBuilderHelper.GetDefaultWebHostBuilder(webApplicationConfiguration, webApplicationStartup);
            return webHostBuilder;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// The <paramref name="configurationServicesProviderProvider"/> provides a service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IWebHostBuilder UseStartup<TStartup>(this WebApplicationBuilder webApplicationBuilder, Func<IServiceProvider> configurationServicesProviderProvider)
            where TStartup : class, IWebApplicationStartup
        {
            var configurationServicesProvider = configurationServicesProviderProvider();

            var webHostBuilder = webApplicationBuilder.UseStartup<TStartup>(configurationServicesProvider);
            return webHostBuilder;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// An empty service provider is provided during the configuration configuration process.
        /// </summary>
        public static IWebHostBuilder UseStartup<TStartup>(this WebApplicationBuilder applicationBuilder)
            where TStartup : class, IWebApplicationStartup
        {
            var webHostBuilder = applicationBuilder.UseStartup<TStartup>(ApplicationBuilderHelper.GetEmptyServiceProvider);
            return webHostBuilder;
        }


        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// The <paramref name="configurationStartupConfigurationServicesProvider"/> provides services during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IWebHostBuilder UseStartup<TStartup, TConfigurationStartup>(this WebApplicationBuilder webApplicationBuilder, IServiceProvider configurationStartupConfigurationServicesProvider)
                where TStartup : class, IWebApplicationStartup
                where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            var applicationBuilder = new ApplicationBuilder(); // Use an application builder for the configuration.

            var configurationServiceProvider = applicationBuilder.UseStartup<TConfigurationStartup>(configurationStartupConfigurationServicesProvider);

            var webHostBuilder = webApplicationBuilder.UseStartup<TStartup>(configurationServiceProvider);
            return webHostBuilder;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// The <paramref name="configurationStartupConfigurationServicesProviderProvider"/> provides a service provider that provides services during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IWebHostBuilder UseStartup<TStartup, TConfigurationStartup>(this WebApplicationBuilder webApplicationBuilder, Func<IServiceProvider> configurationStartupConfigurationServicesProviderProvider)
            where TStartup : class, IWebApplicationStartup
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            var configurationStartupConfigurationServicesProvider = configurationStartupConfigurationServicesProviderProvider();

            var webHostBuilder = webApplicationBuilder.UseStartup<TStartup, TConfigurationStartup>(configurationStartupConfigurationServicesProvider);
            return webHostBuilder;
        }

        /// <summary>
        /// Uses the <typeparamref name="TStartup"/> type configure a service provider instance.
        /// Uses the <typeparamref name="TConfigurationStartup"/> type to configure a service provider that provides services during the configuration configuration process.
        /// An empty service provider is provided during the configuration configuration process of the service provider that provides services during the configuration configuration process.
        /// </summary>
        public static IWebHostBuilder UseStartup<TStartup, TConfigurationStartup>(this WebApplicationBuilder webApplicationBuilder)
            where TStartup : class, IWebApplicationStartup
            where TConfigurationStartup : class, IApplicationConfigurationStartup
        {
            var webHostBuilder = webApplicationBuilder.UseStartup<TStartup, TConfigurationStartup>(ApplicationBuilderHelper.GetEmptyServiceProvider);
            return webHostBuilder;
        }


        #region Derby

        public static IServiceProvider GetApplicationConfigurationConfigurationServiceProvider(this WebApplicationBuilder webApplicationBuilder)
        {
            var applicationBuilder = new ApplicationBuilder();

            var applicationConfigurationConfigurationServiceProvider = applicationBuilder.UseStartup<ApplicationConfigurationConfigurationStartup>();
            return applicationConfigurationConfigurationServiceProvider;
        }

        public static IWebHostBuilder UseStartupWithDerbyConfigurationStartup<TStartup>(this WebApplicationBuilder webApplicationBuilder)
            where TStartup : class, IWebApplicationStartup
        {
            var webHostBuilder = webApplicationBuilder.UseStartup<TStartup, ApplicationConfigurationStartup>(webApplicationBuilder.GetApplicationConfigurationConfigurationServiceProvider);
            return webHostBuilder;
        }

        #endregion

        #region Coventry

        public static IWebHostBuilder UseStartupFromCoventryWithDerbyConfigurationStartup<TStartup>(this WebApplicationBuilder webApplicationBuilder)
            where TStartup : class, IWebApplicationStartup
        {
            var webHostBuilder = webApplicationBuilder.UseStartupWithDerbyConfigurationStartup<TStartup>();
            return webHostBuilder;
        }

        #endregion
    }
}
