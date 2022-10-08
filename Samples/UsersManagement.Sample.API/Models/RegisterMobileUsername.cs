using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Sample.API.Models
{
    public class RegisterMobileUsername
    {
        [Required, MaxLength(20)] //Mobile base 
        public string Mobile { get; set; } = string.Empty;

    }
    public class RegisterEmailUsername
    {
        [Required,MinLength(4)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfimPassword { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
