using UsersManagement.TokenBase.DTOs;

namespace UsersManagement.TokenBase.Common;

public interface IUserMangementService
{

    Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, SignUpDto signUp);
    Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, string password, SignUpDto signUp);
    string GenerateCode(int length = 4, bool digit = true, bool lowercase = false, bool uppercase = false, bool nonAlphanumeric = false);
    
    //Task<SignInResultDto> SignInAsync(string username, SignInDto user, AuthOptions option);
}
