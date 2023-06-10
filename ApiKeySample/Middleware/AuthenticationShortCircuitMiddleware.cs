namespace ApiKeySample.Middleware;

public class AuthenticationShortCircuitMiddleware
{
    private RequestDelegate _next;

    public AuthenticationShortCircuitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity is not null && context.User.Identity.IsAuthenticated)
        {
            await _next(context);       
        }

        context.Response.StatusCode = 401;
    }
}