using Microsoft.AspNetCore.Authorization;

namespace ApiKeySample.Filters;

public class PermissionReadAuthorize : AuthorizeAttribute
{
    public PermissionReadAuthorize()
    {
        Policy = Constants.Permissions.Read;
    }
    
}