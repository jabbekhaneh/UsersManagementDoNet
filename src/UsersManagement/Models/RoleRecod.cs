using System.ComponentModel.DataAnnotations;

namespace UsersManagement.Models
{
    public class RoleRecod
    {
        #region Fields
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime LastActivityDate { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion
    }
}
