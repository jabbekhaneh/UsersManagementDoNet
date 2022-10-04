namespace UsersManagement.TokenBase.Models;
public class UserRecord
{
    public UserRecord(string username)
    {
        this.UserName = username;
    }
    public Guid Id { get; set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; set; }=string.Empty;
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; }=string.Empty;
    public string Token { get; set; }=string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ConfirmCode { get; set; } = string.Empty;
    public string Job { get; set; } = string.Empty;
    public DateTime RegsiterDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime LastActivityDateUtc { get; set; }
    public bool IsActiveMobile { get; set; }
    public bool IsActiveEmail { get; set; }
    public bool IsActive { get; set; }
    public decimal Wallet { get; set; }
}