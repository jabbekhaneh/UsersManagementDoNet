using UsersManagement.TokenBase.Models;

namespace UsersManagement.TokenBase.Options;

public class UsersManagementTokenBaseOption
{

    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string SchemaName { get; set; } = "dbo";
    public bool IsCreateAdminUser { get; set; } = false;
    public UserAdminOption Admin { get; set; }
    public List<PermissionRecord>? Permissions { get; set; }
}
public class UserAdminOption
{
    public UserAdminOption(string username, string passwordHash)
    {
        this.UserName = username;
        this.PasswordHash = passwordHash;
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Database Database { get; set; }
    public string PasswordHash { get; private set; }
    public string UserName { get; private set; }

}
public enum Database : int
{
    MSQL = 1,
}
