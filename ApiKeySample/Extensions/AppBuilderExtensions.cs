using ApiKeySample.Middleware;

namespace ApiKeySample.Extensions;

internal static class AppBuilderExtensions
{
    internal static IApplicationBuilder UseAuthenticationShortCircuit(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<AuthenticationShortCircuitMiddleware>();
        return builder;
    }
}