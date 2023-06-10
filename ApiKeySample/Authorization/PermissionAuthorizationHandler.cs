using System.Security.Claims;
using ApiKeySample.Security;
using Microsoft.AspNetCore.Authorization;

namespace ApiKeySample.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        //Here we check once the api key is authorized to do the action in the specific endpoint  
        Claim? permissionClaim =
            context.User.FindFirst(c =>
                c.Type == CustomClaimTypes.Permission && c.Value.Equals(requirement.Permission));

        if (permissionClaim is not null)
        {
            //It can
            context.Succeed(requirement);
        }
        
        //We do nothing.

        return Task.CompletedTask;
    }
}