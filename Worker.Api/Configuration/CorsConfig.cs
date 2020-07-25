using System;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Worker.Api.Configuration
{
    static class CorsConfig
    {
        public static Action<CorsOptions> GetPolicy()
        {
            return options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            };
        }
    }
}