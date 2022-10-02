namespace UsersManagement.Options;

public sealed class UserManagementOptions
{

    public string ConnectionString { get; set; } = string.Empty;
    public UserManagementUseOption UseOption { get; set; }
    public string SchemaName { get; set; } = "dbo";


}
public enum UserManagementUseOption : int
{
    Default = 1,
    Identity = 2,
    Custome = 3,
}

