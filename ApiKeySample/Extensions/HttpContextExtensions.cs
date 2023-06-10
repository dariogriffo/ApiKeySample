
using Microsoft.Extensions.Primitives;

namespace ApiKeySample.Extensions;

internal static class HttpContextExtensions
{
    internal static bool  TryGetApiKeyFromQueryParams(this HttpContext context, out string parsedApiKey)
    {
        parsedApiKey = string.Empty;
        if (!context.Request.Query.TryGetValue("api_key", out StringValues header))
        {
            return false;
        }

        if (!header.Any())
        {
            return false;
        }

        parsedApiKey = header.First()!;
        return !string.IsNullOrWhiteSpace(parsedApiKey);
    }
}