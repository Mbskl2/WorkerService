using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace Worker.Api.Configuration
{
    static class AuthZeroConfig
    {
        public static Action<JwtBearerOptions> GetJwtBearerOptions(IConfiguration configuration)
        {
            return options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth0:Audience"];
            };
        }

        public static Action<AuthenticationOptions> GetDefaults()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }
    }
}