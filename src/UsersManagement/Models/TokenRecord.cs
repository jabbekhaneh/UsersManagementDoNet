namespace UsersManagement.Models;

public class TokenRecord
{
    public TokenRecord(Guid userId, string token, DateTime expireDate)
    {
        this.Token = token;
        this.ExpireDate = expireDate;
        this.UserId = userId;
    }
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public Guid UserId { get; set; }
}
