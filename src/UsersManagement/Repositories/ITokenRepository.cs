using UsersManagement.Models;

namespace UsersManagement.Repositories;

public interface ITokenRepository
{
    Task<TokenRecord> Create(TokenRecord token);
}
