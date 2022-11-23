using UsersManagement.Models.DTOs;

namespace UsersManagement.Common;

public interface IUserManagement
{
    Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, SignUpDto signUp);
    Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, string password, SignUpDto signUp);
    string CodeGenerator(int length = 4, bool digit = true, bool lowercase = false, bool uppercase = false, bool nonAlphanumeric = false);
    Task<string> TokenGenerator(Guid userId,DateTime expireDate);
    Task<(SignInStatus status,Guid userId)> SignIn(SignInDto signIn);
    
}
