namespace UsersManagement.Models.DTOs;

public enum SignUpStatus : int
{
    DublicateUsername = 1,
    CreateUserSuccess = 2,

}

public enum SignInStatus : int
{
    NotFoundUser = 1,
    Success = 2,
    UsernameIncorrect = 3,
    PasswordIncorrect=4,
    


}