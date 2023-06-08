using ApiKeySample.Authentication;
using ApiKeySample.Authorization;
using ApiKeySample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiKeySample.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApiKey(this IServiceCollection services)
    {
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = Constants.ApiKeyScheme;
                x.DefaultChallengeScheme = Constants.ApiKeyScheme;
            })
            .AddScheme<ApiKeySchemeOptions, ApiKeySchemeHandler>(
                Constants.ApiKeyScheme,
                Constants.ApiKeyScheme,
                null);

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy(
                Constants.Permissions.Read,
                policy => policy.Requirements.Add(new PermissionRequirement(Constants.Permissions.Read)));

            options.AddPolicy(
                Constants.Permissions.Write,
                policy => policy.Requirements.Add(new PermissionRequirement(Constants.Permissions.Write)));
        });


        services
            .AddSingleton<IApiKeyStore, ApiKeyStore>()
            .AddSingleton<IClaimsProvider, ClaimsProvider>()
            .AddSingleton<IAuthorizationMiddlewareResultHandler, PermissionAuthorizationMiddlewareResultHandler>()
            .AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        return services;
    }
}