using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace Worker.Api.Configuration
{
    static class SpaConfig
    {
        public static Action<SpaStaticFilesOptions> GetOptions()
        {
            return configuration => {
                configuration.RootPath = "ClientApp / dist";
            };
        }

        public static void UseSpaConfig(this IApplicationBuilder app, bool isDevelopment)
        {
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (isDevelopment)
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            });
        }
    }
}