namespace ApiKeySample.Security;


/// <summary>
/// The ApiKey, this can be loaded from any type of Store: DB/Cache
/// </summary>
public class ApiKey
{
    public ApiKey(string key, Guid companyId, string permissions)
    {
        Key = key;
        CompanyId = companyId;
        Permissions = permissions.Select(x => x.ToString()).ToArray();
    }

    public string Key { get; }

    public Guid CompanyId { get; }
    public string[] Permissions { get; }
}