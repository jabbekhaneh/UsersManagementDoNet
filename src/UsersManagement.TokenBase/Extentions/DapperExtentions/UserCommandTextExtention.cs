namespace UsersManagement.TokenBase.Extentions.DapperExtentions;

internal static class UserCommandTextExtention
{
    private static string TableName = "Users";
    public static string CreateTableQuery(string schema="dbo")
        =>       $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                 $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{TableName}' )) Begin " +
                 $"CREATE TABLE [{schema}].[{TableName}]( " +
                 $"Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID())," +
                 $"[UserName] [nvarchar](255) NOT NULL UNIQUE," +
                 $"[PasswordHash] [nvarchar](255) NULL," +
                 $"[FirstName] [nvarchar](255) NULL," +
                 $"[LastName] [nvarchar](255) NULL," +
                 $"[Mobile] [nvarchar](20) NULL," +
                 $"[Email] [nvarchar](255) NULL," +
                 $"[Token] [nvarchar](1000) NULL," +
                 $"[Address] [nvarchar](500) NULL," +
                 $"[ConfirmCode] [nvarchar](10) NULL," +
                 $"[Job] [nvarchar](255) NULL," +
                 $"[RegsiterDate] [datetime2](7) NULL," +
                 $"[UpdateDate] [datetime2](7) NULL," +
                 $"[LastActivityDateUtc] [datetime2](7) NULL," +
                 $"[IsActive] [bit] ," +
                 $"[IsActiveMobile] [bit]," +
                 $"[IsActiveEmail] [bit]," +
                 $"[Wallet] [decimal](19, 5) NULL default(0)" +
                 $")" +
                 $" End";


    public static string IsExistByUsernameQuery
       => $"select count(1) from Users where UserName=@UserName";

    public static string IsExistByMobileQuery
       => $"select count(1) from Users where Mobile=@Mobile";

    public static string IsExistByEmailQuery =>
         $"select count(1) from Users where Email=@Email";
    

    public static string InsertQuery => $"INSERT INTO Users (UserName,PasswordHash,FirstName,LastName,Mobile,Email,Token,Address,ConfirmCode," +
                                 $"Job,RegsiterDate,UpdateDate,LastActivityDateUtc,IsActive,IsActiveMobile,IsActiveEmail,Wallet)" +
                                 $"VALUES (@UserName,@PasswordHash,@FirstName,@LastName,@Mobile,@Email,@Token,@Address,@ConfirmCode," +
                                 $"@Job,@RegsiterDate,@UpdateDate,@LastActivityDateUtc,@IsActive,@IsActiveMobile,@IsActiveEmail,@Wallet)";
}
