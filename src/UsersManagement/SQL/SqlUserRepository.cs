using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using UsersManagement.Models;
using UsersManagement.Options;
using UsersManagement.Repositories;
using UsersManagement.SQL.TextCommands;

namespace UsersManagement.SQL
{
    public class SqlUserRepository : IUserRepository
    {
        private IDbConnection _dbConnection;
        private readonly IOptions<UserManagementOptions> _options;
        public SqlUserRepository(IOptions<UserManagementOptions> options)
        {
            _options = options;
            if (string.IsNullOrEmpty(options.Value.ConnectionString))
                throw new ArgumentNullException($"{nameof(options.Value.ConnectionString)} Checking UsersManagementTokenBaseService");
            _dbConnection = new SqlConnection(options.Value.ConnectionString);
        }
        //------------------------------
        public Guid CreateUser(UserRecord user)
        {
            var sqlQuery = UserCommandText.InsertINTO;
            var newUser = MapNewUser(user);
            var id = _dbConnection.QuerySingle<Guid>(sqlQuery, newUser);
            return id;
        }
        //------------------------------
        public async Task<Guid> CreateUserAsync(UserRecord user)
        {
            var sqlQuery = UserCommandText.InsertINTO;
            var newUser = MapNewUser(user);
            newUser.Id = Guid.NewGuid();
            await _dbConnection.ExecuteAsync(sqlQuery, newUser);
            return newUser.Id;
        }
        //------------------------------
        private static UserRecord MapNewUser(UserRecord user)
        {
            return new UserRecord(user.UserName)
            {
                PasswordHash = user.PasswordHash,
                RegsiterDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Token = user.Token,
                Mobile = user.Mobile,
                Job = user.Job,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LastActivityDateUtc = DateTime.Now,
                ConfirmCode = user.ConfirmCode,
                Email = user.Email,
                IsActive = false,
                IsActiveEmail = false,
                IsActiveMobile = false,
                Wallet = 0,
            };
        }
        //------------------------------
        public bool IsExistByUserName(string username)
        {
            return _dbConnection
                .ExecuteScalar<bool>(UserCommandText
                .IsExistByUsernameQuery, new { username });
        }
        //------------------------------
        public async Task<bool> IsExistByUserNameAsync(string username)
        {
            return await _dbConnection
                .ExecuteScalarAsync<bool>(UserCommandText
                .IsExistByUsernameQuery, new { username });
        }
        //------------------------------
        public bool IsExistByMobile(string mobile)
        {
            return _dbConnection
                .ExecuteScalar<bool>(UserCommandText
                .IsExistByMobileQuery, new { mobile });
        }
        //------------------------------
        public async Task<bool> IsExistByMobileAsync(string mobile)
        {
            return await _dbConnection
                .ExecuteScalarAsync<bool>(UserCommandText
                .IsExistByMobileQuery, new { mobile });
        }
        //------------------------------
        public bool IsExistByEmail(string email)
        {
            return _dbConnection
                .ExecuteScalar<bool>(UserCommandText
                .IsExistByEmailQuery, new { email });
        }
        //------------------------------
        public async Task<bool> IsExistByEmailAsync(string email)
        {
            return await _dbConnection
                .ExecuteScalarAsync<bool>(UserCommandText
                .IsExistByEmailQuery, new { email });
        }
        //------------------------------
        public async Task Update(Guid userId, UserRecord user)
        {
            await _dbConnection.ExecuteAsync(UserCommandText
                 .Update(), user);

        }
        //------------------------------
    }
}
