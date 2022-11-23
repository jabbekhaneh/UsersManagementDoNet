using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models
{
    public class UserLoginHistoryRecord
    {

        #region Fields
        public Guid Id { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string? OS { get; set; }
        public string? ShortMessage { get; set; }
        public LoginHistoryStatus Status { get; set; }
        public DateTime LoginDate { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public string? Token { get; set; }
        public DateTime ExpireTokenDate { get; set; }

        #endregion


    }

    public enum LoginHistoryStatus : int
    {
        Success = 1,
        WrongPassword = 2,
        WrongConfimCode=3,
        

    }
}
