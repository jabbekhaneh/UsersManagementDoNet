namespace UsersManagement.TokenBase.DTOs;

public class CreateUser
{
    public CreateUser(string username)
    {
        if (username == null)
            throw new ArgumentNullException("username");
        this.UserName = username;
    }
    public string UserName { get;  private set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ConfirmCode { get; set; } = string.Empty;
    public string Job { get; set; } = string.Empty;
    public bool IsActiveMobile { get; set; }
    public bool IsActiveEmail { get; set; }
    public bool IsActive { get; set; } 
    public decimal Wallet { get; set; } = 0;
   
}


public class UpdateUser
{
    public UpdateUser(Guid userId)
    {
        this.Id=userId;
        if (Id == Guid.Empty)
            throw new ArgumentException("userId");
    }
    public Guid Id { get; private set; } = Guid.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ConfirmCode { get; set; } = string.Empty;
    public string Job { get; set; } = string.Empty;
    public bool IsActiveMobile { get; set; }
    public bool IsActiveEmail { get; set; }
    public bool IsActive { get; set; }
    public decimal Wallet { get; set; }
}