using System.ComponentModel.DataAnnotations;

namespace UsersManagement.TokenBase.DTOs;

public class SignInDto
{
    
}
public class SignUpDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string ConfimCode { get; set; } = string.Empty;
    public string Token { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;

}
