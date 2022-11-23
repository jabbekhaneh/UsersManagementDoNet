using Dapper;
using System.Data.SqlClient;
using UsersManagement.Models;
using UsersManagement.Options;
using UsersManagement.SQL.TextCommands;

namespace UsersManagement.Configurations;

public class UserManagementConfigurations
{
    private readonly UserManagementOptions _configuration;
    public UserManagementConfigurations(UserManagementOptions configuration)
    {
        _configuration = configuration;
    }

    public UserManagementConfigurations CreateTablesMSQL()
    {
        if (_configuration.Database == DatabaseType.MSQL)
        {
            if (_configuration.ConnectionString == null)
                throw new ArgumentNullException("ConnectionString userManagement is null");
            ConnectedDatabase();
        }
        return this;
    }

    private void ConnectedDatabase()
    {
        var createTokenQuery = TokenCommandText.CreateTableQuery(_configuration.SchemaName);
        //
        var createUserQuery = UserCommandText.CreateTableQuery(_configuration.SchemaName);
        var isExistUsernameQuery = UserCommandText.IsExistByUsernameQuery;
        //
        var createUserHistory = UserLoginHistoryCommandText.CreateTableQuery(_configuration.SchemaName);
        using (var connection = new SqlConnection(_configuration.ConnectionString))
        {
            connection.Open();
            connection.Execute(createTokenQuery);
            connection.Execute(createUserQuery);
            connection.Execute(createUserHistory);
            
            bool isExistUserName = connection.ExecuteScalar<bool>(isExistUsernameQuery,
                new { username = _configuration.Admin?.UserName });
            if (_configuration.IsCreateAdminUser && !isExistUserName)
                connection.Execute(UserCommandText.InsertINTO, MapUserAdmin(_configuration.Admin));
            //connection.Close();
        }
    }

    private static UserRecord MapUserAdmin(UserAdminOption admin)
    {
        return new UserRecord(admin.UserName)
        {
            Id = Guid.NewGuid(),
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
}
