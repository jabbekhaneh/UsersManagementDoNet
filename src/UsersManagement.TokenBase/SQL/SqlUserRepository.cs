using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using UsersManagement.TokenBase.DTOs;
using UsersManagement.TokenBase.Extentions.DapperExtentions;
using UsersManagement.TokenBase.Models;
using UsersManagement.TokenBase.Options;
namespace UsersManagement.TokenBase.SQL;
public class SqlUserRepository : IUserRepository
{
    private  IDbConnection _dbConnection;
    private readonly IOptions<UsersManagementTokenBaseOption> _options;
    public SqlUserRepository(IOptions<UsersManagementTokenBaseOption> options)
    {
        _options = options;
        if (string.IsNullOrEmpty(options.Value.ConnectionString))
            throw new ArgumentNullException($"{nameof(options.Value.ConnectionString)} Checking UsersManagementTokenBaseService");
        _dbConnection = new SqlConnection(options.Value.ConnectionString);
        
        CreateTable();
        SeadData(options.Value);
    }
    //------------------------------
    #region ConfigOptions
    private void SeadData(UsersManagementTokenBaseOption option)
    {
        if (option.IsCreateAdminUser)
            CreateAdmin(option.Admin);
        if (option.Permissions?.Count() > 0)
            CreatePermissions();

    }

    private void CreatePermissions()
    {
        throw new NotImplementedException();
    }

    private void CreateAdmin(UserAdminOption admin)
    {
        var existUserName = _dbConnection
            .ExecuteScalar<bool>(UserCommandTextExtention.IsExistByUsernameQuery, new { admin.UserName });
        if (!existUserName)
        {
            _dbConnection.Execute(UserCommandTextExtention.InsertQuery, MapUserAdmin(admin));
        }
        else
        {
            //ToDo log dublicate username
        }
    }

    private static UserRecord MapUserAdmin(UserAdminOption admin)
    {
        return new UserRecord(admin.UserName)
        {
            PasswordHash = admin.PasswordHash,
            Mobile = admin.Mobile,
            Email = admin.Email,
            FirstName = admin.FirstName,
            LastName = admin.LastName,
            RegsiterDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            LastActivityDateUtc = DateTime.Now,
        };
    }

    private void CreateTable()
    {
        var query = UserCommandTextExtention.CreateTableQuery(_options.Value.SchemaName);
        _dbConnection.Execute(query);
    }
    #endregion
    //------------------------------
    public Guid CreateUser(CreateUser user)
    {
        var sqlQuery = UserCommandTextExtention.InsertQuery;
        var newUser = MapNewUser(user);
        _dbConnection.Execute(sqlQuery, newUser);
        return newUser.Id;
    }
    //------------------------------
    public async Task<Guid> CreateUserAsync(CreateUser user)
    {
        var sqlQuery = UserCommandTextExtention.InsertQuery;
        var newUser = MapNewUser(user);
        await _dbConnection.ExecuteAsync(sqlQuery, newUser);
        return newUser.Id;
    }
    //------------------------------
    private static UserRecord MapNewUser(CreateUser user)
    {
        return new UserRecord(user.UserName)
        {
            Id = Guid.NewGuid(),
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
            .ExecuteScalar<bool>(UserCommandTextExtention
            .IsExistByUsernameQuery, new { username });
    }
    //------------------------------
    public async Task<bool> IsExistByUserNameAsync(string username)
    {
        return await _dbConnection
            .ExecuteScalarAsync<bool>(UserCommandTextExtention
            .IsExistByUsernameQuery, new { username });
    }
    //------------------------------
    public bool IsExistByMobile(string mobile)
    {
        return  _dbConnection
            .ExecuteScalar<bool>(UserCommandTextExtention
            .IsExistByMobileQuery, new { mobile });
    }
    //------------------------------
    public async Task<bool> IsExistByMobileAsync(string mobile)
    {
        return await _dbConnection
            .ExecuteScalarAsync<bool>(UserCommandTextExtention
            .IsExistByMobileQuery, new { mobile });
    }
    //------------------------------
    public bool IsExistByEmail(string email)
    {
        return _dbConnection
            .ExecuteScalar<bool>(UserCommandTextExtention
            .IsExistByEmailQuery, new { email });
    }
    //------------------------------
    public async Task<bool> IsExistByEmailAsync(string email)
    {
        return await _dbConnection
            .ExecuteScalarAsync<bool>(UserCommandTextExtention
            .IsExistByEmailQuery, new { email });
    }
    //------------------------------
    public async Task Update(Guid userId,UpdateUser user)
    {
        using (var connection = new SqlConnection(_options.Value.ConnectionString))
        {
            connection.Open();
            var result = await connection.ExecuteAsync(UserCommandTextExtention
            .Update(), new UpdateUser(userId)
            {

                ConfirmCode = user.ConfirmCode,


            });
            
        }
        
    }
    //------------------------------
}
