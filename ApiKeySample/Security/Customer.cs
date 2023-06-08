using System.Security.Claims;
using System.Security.Principal;

namespace ApiKeySample.Security;

/// <summary>
/// A logged in Principal that abstract claims
/// </summary>
public class Customer : ClaimsPrincipal
{
    private readonly ApiKey _apiKey;

    public Customer(IIdentity principal, ApiKey apiKey)
        : base(principal)
    {
        _apiKey = apiKey;
    }

    public string Id => _apiKey.Key;

    public Guid CompanyId => _apiKey.CompanyId;

    public bool CanWrite() =>
        Claims.Any(x => x is { Type: CustomClaimTypes.Permission, Value: Constants.Permissions.Write });
    
    public bool CanRead() =>
        Claims.Any(x => x is { Type: CustomClaimTypes.Permission, Value: Constants.Permissions.Read });
}

