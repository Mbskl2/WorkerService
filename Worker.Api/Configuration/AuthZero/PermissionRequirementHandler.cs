using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Worker.Api.Configuration.AuthZero
{
    class PermissionRequirementHandler: AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (HasRequiredPermission(context.User, requirement))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private bool HasRequiredPermission(ClaimsPrincipal user, PermissionRequirement requirement)
        {
            var permissions = user.FindAll(
                c => c.Type == "permissions" && c.Issuer == requirement.Issuer);
            return permissions.Any(s => s.Value == requirement.Permission);
        }

    }
}