using System.Security.Claims;
using ApiKeySample.Authorization;
using ApiKeySample.Security;

namespace ApiKeySample.Services;

internal class ClaimsProvider : IClaimsProvider
{
    public IEnumerable<Claim> GetClaimsFor(ApiKey apiKey)
    {
        return
            apiKey
                .Permissions
                .Select(c => c switch
                {
                    Constants.Permissions.Read => new Claim(CustomClaimTypes.Permission, Constants.Permissions.Read),
                    Constants.Permissions.Write => new Claim(CustomClaimTypes.Permission, Constants.Permissions.Write),
                    _ => throw new ArgumentException()
                });
    }
}
