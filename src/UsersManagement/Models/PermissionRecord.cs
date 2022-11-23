using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models;

public class PermissionRecord
{
    #region Fields
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    #endregion
}
