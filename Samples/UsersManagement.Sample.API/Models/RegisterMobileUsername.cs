using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Sample.API.Models
{
    public class RegisterMobileUsername
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required,MaxLength(20)] //Mobile base 
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
