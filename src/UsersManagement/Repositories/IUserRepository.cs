using UsersManagement.Models;

namespace UsersManagement.Repositories;

public interface IUserRepository
{
    Guid CreateUser(UserRecord user);
    Task<Guid> CreateUserAsync(UserRecord user);
    bool IsExistByUserName(string username);
    Task<bool> IsExistByUserNameAsync(string username);
    bool IsExistByMobile(string mobile);
    Task<bool> IsExistByMobileAsync(string mobile);
    bool IsExistByEmail(string email);
    Task<bool> IsExistByEmailAsync(string email);
    Task Update(Guid userId, UserRecord user);
    Task<UserRecord> FindByUserNameAsync(string username);

}
