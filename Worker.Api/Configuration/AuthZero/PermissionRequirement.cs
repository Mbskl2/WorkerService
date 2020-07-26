using System;
using Microsoft.AspNetCore.Authorization;

namespace Worker.Api.Configuration.AuthZero
{
    class PermissionRequirement: IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Permission { get; }

        public PermissionRequirement(string permission, string issuer)
        {
            Permission = permission;
            Issuer = issuer;
        }
    }
}