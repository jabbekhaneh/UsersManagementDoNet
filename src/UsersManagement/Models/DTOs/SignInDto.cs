using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models.DTOs;

public class SignInDto
{
    [Required]
    public string UserName { get; set; }=string.Empty;
    [Required]
    public string PasswordHash { get; set; }=string.Empty;
}
