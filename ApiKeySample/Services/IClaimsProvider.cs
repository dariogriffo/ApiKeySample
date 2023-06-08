using System.Security.Claims;
using ApiKeySample.Authorization;
using ApiKeySample.Security;

namespace ApiKeySample.Services;

public interface IClaimsProvider
{
    IEnumerable<Claim> GetClaimsFor(ApiKey apiKey);
}