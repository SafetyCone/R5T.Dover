using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;

using R5T.Richmond;





namespace R5T.Dover
{
    public class WebApplicationBuilder
    {
        #region Static

        public static WebApplicationBuilder New()
        {
            var webApplicationBuilder = new WebApplicationBuilder();
            return webApplicationBuilder;
        }

        #endregion
    }
}
