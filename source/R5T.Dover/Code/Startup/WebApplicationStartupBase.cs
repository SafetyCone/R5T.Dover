using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using CoventryApplicationStartupBase = R5T.Coventry.ApplicationStartupBase;


namespace R5T.Dover
{
    public class WebApplicationStartupBase : CoventryApplicationStartupBase, IWebApplicationStartup
    {
        public WebApplicationStartupBase(ILogger<WebApplicationStartupBase> logger)
            : base(logger)
        {
        }

        public void Configure(IApplicationBuilder applicationBuilder)
        {
            // Call the application startup base-class configure method on the application's services.
            base.Configure(applicationBuilder.ApplicationServices);

            var hostingEnvironment = applicationBuilder.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            // Now call the ASP.NET Core web-application configure method.
            this.Logger.LogDebug("Starting configure of web-application service provider...");

            this.ConfigureBody(applicationBuilder, hostingEnvironment);

            this.Logger.LogDebug("Finished configure of web-application service provider.");
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureBody(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            // Do nothing.
        }
    }
}
