using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Models;
using UsersManagement.Options;
using UsersManagement.Repositories;
using UsersManagement.SQL.TextCommands;

namespace UsersManagement.SQL
{
    public class SqlTokenRepository : ITokenRepository
    {
        private IDbConnection _dbConnection;
        private readonly IOptions<UserManagementOptions> _options;
        public SqlTokenRepository(IOptions<UserManagementOptions> options)
        {
            _options = options;
            _dbConnection = new SqlConnection(options.Value.ConnectionString);
        }
        public async Task<TokenRecord> Create(TokenRecord token)
        {
            await _dbConnection.ExecuteAsync(TokenCommandText
                .InsertINTO, token);
            return token;
        }
    }
}
