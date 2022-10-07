using UsersManagement.TokenBase.DTOs;

namespace UsersManagement.TokenBase.Common
{
    public interface IUserRepository
    {
        Guid CreateUser(CreateUser user);
        Task<Guid> CreateUserAsync(CreateUser user);
        bool IsExistByUserName(string username);
        Task<bool> IsExistByUserNameAsync(string username);
        bool IsExistByMobile(string mobile);
        Task<bool> IsExistByMobileAsync(string mobile);
        bool IsExistByEmail(string email);
        Task<bool> IsExistByEmailAsync(string email);
        Task Update(Guid userId,UpdateUser user);

    }
}
