using UsersManagement.TokenBase.DTOs;
using UsersManagement.TokenBase.Models;

namespace UsersManagement.TokenBase.Common
{
    public interface IUserRepository
    {
        Guid CreateUser(CreateUser user);
        Task<Guid> CreateUserAsync(CreateUser user);

    }
}
