using Microsoft.Extensions.Options;
using System.Text;
using UsersManagement.Common;
using UsersManagement.Configurations;
using UsersManagement.Models;
using UsersManagement.Models.DTOs;
using UsersManagement.Options;
using UsersManagement.Repositories;

namespace UsersManagement.Services
{
    public class UserManagement : IUserManagement
    {
        private IUserRepository _repository;
        private readonly ITokenRepository _tokenRepository;
        private readonly UserManagementConfigurations _configurations;
        public UserManagement(IOptions<UserManagementOptions> options,
            IUserRepository repository,
            ITokenRepository tokenRepository)
        {
            _configurations = new UserManagementConfigurations(options.Value)
                .CreateTablesMSQL();
            
            _repository = repository;
            _tokenRepository = tokenRepository;
        }
        //-----------------------------------
        public async Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, SignUpDto signUp)
        {
            if (await _repository.IsExistByUserNameAsync(username))
                return (SignUpStatus.DublicateUsername, Guid.Empty);
            var newUser = MapUser(username, signUp);
            var userId = await _repository.CreateUserAsync(newUser);
            return (SignUpStatus.CreateUserSuccess, userId);
        }
        //-----------------------------------
        private static UserRecord MapUser(string username, SignUpDto dto, string passwordHash = "")
        {
            return new UserRecord(username.ToLower().Trim())
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
                ConfirmCode = dto.ConfimCode,
            };
        }
        //-----------------------------------
        public string CodeGenerator(int length = 4, bool digit = true, bool lowercase = false, bool uppercase = false, bool nonAlphanumeric = false)
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
        //-----------------------------------
        public async Task<(SignUpStatus signUpStatus, Guid userId)> SignUpAsync(string username, string password, SignUpDto signUp)
        {
            if (await _repository.IsExistByUserNameAsync(username))
                return (SignUpStatus.DublicateUsername, Guid.Empty);
            var newUser = MapUser(username, signUp, password.EncryptString());
            var userId = await _repository.CreateUserAsync(newUser);
            return (SignUpStatus.CreateUserSuccess, userId);
        }
        //-----------------------------------
        public async Task<string> TokenGenerator(Guid userId, DateTime expireDate)
        {
            string tokenStr = Guid.NewGuid().ToString();
            await _tokenRepository.Create(new TokenRecord(userId,
                tokenStr, expireDate)
            {
                CreateDate = DateTime.Now,
                Id = Guid.NewGuid(),
            });
            return tokenStr;
        }
        //-----------------------------------
        public async Task<(SignInStatus status, Guid userId)> SignIn(SignInDto signIn)
        {
            if (await _repository.IsExistByUserNameAsync(signIn.UserName))
                return (SignInStatus.UsernameIncorrect, Guid.Empty);
            var user =await _repository.FindByUserNameAsync(signIn.UserName);
            if(user.PasswordHash != signIn.PasswordHash)
                return (SignInStatus.PasswordIncorrect, Guid.Empty);
            return (SignInStatus.Success, user.Id);
        }
        //-----------------------------------
    }
}
