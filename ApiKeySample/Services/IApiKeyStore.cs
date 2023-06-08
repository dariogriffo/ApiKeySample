using ApiKeySample.Authorization;
using ApiKeySample.Security;

namespace ApiKeySample.Services;

public interface IApiKeyStore
{
    Task<ApiKey?> Get(string apiKey, CancellationToken cancellationToken);
}