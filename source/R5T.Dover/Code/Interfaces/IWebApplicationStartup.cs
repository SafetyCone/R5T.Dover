using System;

using Microsoft.AspNetCore.Builder;

using R5T.Richmond;


namespace R5T.Dover
{
    public interface IWebApplicationStartup : IApplicationStartup
    {
        void Configure(IApplicationBuilder applicationBuilder);
    }
}
