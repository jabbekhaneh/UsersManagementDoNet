using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.SQL.TextCommands;

internal class TokenCommandText
{
    private static string TableName = "Tokens";
    //------------------------------------------------
    public static string CreateTableQuery(string schema = "dbo")
        => $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                 $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{TableName}' )) Begin " +
                 $"CREATE TABLE [{schema}].[{TableName}]( " +
                 $"Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID())," +
                 $"[Token] [nvarchar](1000) NOT NULL UNIQUE," +
                 $"[CreateDate] [datetime2] NULL," +
                 $"[ExpireDate] [datetime2] NULL," +
                 $"[UserId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(Id)" +
                 $")" +
                 $" End";

    //------------------------------------------------
    public static string InsertINTO => $"INSERT INTO {TableName} (Token,CreateDate,ExpireDate,UserId)" +
                            $"VALUES (@Token,@CreateDate,@ExpireDate,@UserId)";
}
