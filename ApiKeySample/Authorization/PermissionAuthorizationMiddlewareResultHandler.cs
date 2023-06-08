using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace ApiKeySample.Authorization;

public class PermissionAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden
            && authorizeResult.AuthorizationFailure!.FailedRequirements
                .OfType<PermissionRequirement>().Any())
        {
            // Authenticated, but cannot access the resource => 403
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        // Fall back to the default implementation to continue the flow
        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}
