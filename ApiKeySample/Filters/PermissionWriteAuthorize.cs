using Microsoft.AspNetCore.Authorization;

namespace ApiKeySample.Filters;

public class PermissionWriteAuthorize : AuthorizeAttribute
{
    public PermissionWriteAuthorize()
    {
        Policy = Constants.Permissions.Write;
    }
}