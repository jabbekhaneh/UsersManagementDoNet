using Microsoft.Extensions.Options;
using System.Text;
using UsersManagement.TokenBase.DTOs;
using UsersManagement.TokenBase.Extentions;
using UsersManagement.TokenBase.Options;

namespace UsersManagement.TokenBase.Services;

public class UserMangementTokenBaseService : IUserMangementService
{
    private IUserRepository _repository;
    private readonly JwtAuthenticationToken _jwtToken;
    private readonly IOptions<UsersManagementTokenBaseOption> _options;
    public UserMangementTokenBaseService(IUserRepository repository,
        JwtAuthenticationToken jwtToken,
        IOptions<UsersManagementTokenBaseOption> options)
    {
        _repository = repository;
        _jwtToken = jwtToken;
        _options = options;
    }
    //-----------------------------------
    public async Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, SignUpDto signUp)
    {
        if (await _repository.IsExistByUserNameAsync(username))
            return (SignUpStatus.DublicateUsername, Guid.Empty);
        CreateUser createUser = MapCreateUser(username, signUp);
        var userId = await _repository.CreateUserAsync(createUser);
        return (SignUpStatus.CreateUserSuccess, userId);
    }
    //-----------------------------------
    private static CreateUser MapCreateUser(string username, SignUpDto dto, string passwordHash = "")
    {
        return new CreateUser(username.ToLower().Trim())
        {
            IsActive = false,
            IsActiveEmail = false,
            IsActiveMobile = false,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PasswordHash = (string.IsNullOrEmpty(passwordHash) ? string.Empty : passwordHash),
            Wallet = 0,
            Mobile = dto.Mobile,
            ConfirmCode=dto.ConfimCode,
            Token = dto.Token,
        };
    }
    //-----------------------------------
    public string GenerateCode(int length = 4, bool digit = true, bool lowercase = false, bool uppercase = false, bool nonAlphanumeric = false)
    {
        return CodeGeneretor(length, digit, lowercase, uppercase, nonAlphanumeric);
    }
    //-----------------------------------
    public async Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, string password, SignUpDto signUp)
    {
        if (await _repository.IsExistByUserNameAsync(username))
            return (SignUpStatus.DublicateUsername, Guid.Empty);
        CreateUser createUser = MapCreateUser(username, signUp,password.EncryptString());
        var userId = await _repository.CreateUserAsync(createUser);
        return (SignUpStatus.CreateUserSuccess, userId);
    }
    //-----------------------------------
    private static string CodeGeneretor(int length = 1, bool digit = true, bool lowercase = false, bool uppercase = false, bool nonAlphanumeric = false)
    {
        StringBuilder password = new StringBuilder();
        Random random = new Random();

        while (password.Length < length)
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 4);
            if (num == 1)
            {
                if (digit == true)
                {
                    password.Append((char)random.Next(48, 58));//digit
                }
            }
            if (num == 2)
            {
                if (lowercase == true)
                {
                    password.Append((char)random.Next(97, 123));//lowercase

                }
            }
            if (num == 3)
            {
                if (uppercase == true)
                {
                    password.Append((char)random.Next(65, 91));//uppercase
                }
            }
            if (num == 4)
            {
                if (nonAlphanumeric == true)
                {
                    password.Append((char)random.Next(33, 48));//nonAlphanumeric
                }
            }

        }
        return password.ToString();
    }

}
