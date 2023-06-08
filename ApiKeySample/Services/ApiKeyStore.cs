using ApiKeySample.Authorization;
using ApiKeySample.Security;

namespace ApiKeySample.Services;

internal class ApiKeyStore : IApiKeyStore
{
    private static Guid ReadWrite = Guid.Parse("00000000-0000-0000-0000-000000000000");
    private static Guid ReadOnly = Guid.Parse("00000000-0000-0000-0000-000000000001");

    private Dictionary<string, ApiKey> _keys = new()
    {
        { $"{ReadWrite}", new ApiKey($"{ReadWrite}", ReadWrite, "rw") },
        { $"{ReadOnly}", new ApiKey($"{ReadOnly}", ReadOnly, "r") },
    };

    public Task<ApiKey?> Get(string apiKey, CancellationToken cancellationToken)
    {
        _keys.TryGetValue(apiKey, out ApiKey? key);
        return Task.FromResult(key);
    }
}
