namespace UsersManagement.SQL.TextCommands;

public static class UserLoginHistoryCommandText
{
    private static string TableName = "UserLoginHistories";
    //------------------------------------------------
    public static string CreateTableQuery(string schema)
        => $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                 $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{TableName}' )) Begin " +
                 $"CREATE TABLE [{schema}].[{TableName}]( " +
                 $"Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID())," +
                 $"[Token] [nvarchar](1000) NULL UNIQUE," +
                 $"[LoginDate] [datetime2] NULL," +
                 $"[ExpireTokenDate] [datetime2] NULL," +
                 $"[Status] [int] NULL," +
                 $"[OS] [nvarchar](255) NULL," +
                 $"[ShortMessage] [nvarchar](500) NULL," +
                 $"[IpAddress] [nvarchar](255) NULL," +
                 $"[UserId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(Id)" +
                 $")" +
                 $" End";
}
