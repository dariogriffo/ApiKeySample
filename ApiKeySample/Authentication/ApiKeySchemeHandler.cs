using System.Security.Claims;
using System.Text.Encodings.Web;
using ApiKeySample.Authorization;
using ApiKeySample.Extensions;
using ApiKeySample.Security;
using ApiKeySample.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;

namespace ApiKeySample.Authentication;

public class ApiKeySchemeHandler : AuthenticationHandler<ApiKeySchemeOptions>
{
    private readonly IApiKeyStore _apiKeyStore;
    private readonly IClaimsProvider _claimsProvider;

    public ApiKeySchemeHandler(
        IOptionsMonitor<ApiKeySchemeOptions> options,
        ILoggerFactory loggerFactory,
        UrlEncoder encoder,
        ISystemClock clock,
        IApiKeyStore apiKeyStore,
        IClaimsProvider claimsProvider) : base(options, loggerFactory, encoder, clock)
    {
        _apiKeyStore = apiKeyStore;
        _claimsProvider = claimsProvider;
    }

    protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        return Task.CompletedTask;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (RequestIsNotRequiredToAuthenticateWithApiKey())
        {
            //This request doesn't required AUTH so pass on the ticket to the middleware
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), Scheme.Name));
        }
        
        if (!Context.TryGetApiKeyFromQueryParams(out string value))
        {
            //Missing key the way we want it
            return AuthenticateResult.Fail("");
        }

        //Let's find our api key based on the parameter
        CancellationToken cancellationToken = Context.RequestAborted;
        ApiKey? apiKey = await _apiKeyStore.Get(value, cancellationToken);

        if (apiKey is null)
        {
            //Unrecognized key
            return AuthenticateResult.Fail("");
        }

        // Generate AuthenticationTicket from the Identity and current authentication scheme
        IEnumerable<Claim> claims = _claimsProvider.GetClaimsFor(apiKey);
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, nameof(ApiKeySchemeHandler));
        AuthenticationTicket ticket = new AuthenticationTicket(new Customer(claimsIdentity, apiKey), this.Scheme.Name);

        // Pass on the ticket to the middleware. All good, main door is open
        return AuthenticateResult.Success(ticket);
    }

    private bool RequestIsNotRequiredToAuthenticateWithApiKey()
    {
        return Context.Request.GetDisplayUrl() == "/health";
    }
}
