using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static Worker.Api.Configuration.AuthZero.AuthZeroPermissions;

namespace Worker.Api.Configuration.AuthZero
{
    static class AuthZeroConfig
    {
        public static void AddAuthZeroConfig(this IServiceCollection services, IConfiguration configuration)
        {
            string domain = $"https://{configuration["Auth0:Domain"]}/";
            string audience = configuration["Auth0:ApiIdentifier"];

            services.AddAuthentication(AuthZeroConfig.GetDefaults())
                .AddJwtBearer(AuthZeroConfig.GetJwtBearerOptions(domain, audience));
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ReadWorkers, policy => policy.Requirements.Add(new PermissionRequirement(ReadWorkers, domain)));
                options.AddPolicy(CreateWorkers, policy => policy.Requirements.Add(new PermissionRequirement(CreateWorkers, domain)));
                options.AddPolicy(SearchWorkers, policy => policy.Requirements.Add(new PermissionRequirement(SearchWorkers, domain)));
                options.AddPolicy(ModifyWorkers, policy => policy.Requirements.Add(new PermissionRequirement(ModifyWorkers, domain)));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        }

        private static Action<JwtBearerOptions> GetJwtBearerOptions(string domain, string audience)
        {
            return options =>
            {
                options.Authority = domain;
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            };
        }

        private static Action<AuthenticationOptions> GetDefaults()
        {
            return options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            };
        }

    }
}