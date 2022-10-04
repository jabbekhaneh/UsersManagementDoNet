global using UsersManagement.TokenBase.Common;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.TokenBase.Options;
using UsersManagement.TokenBase.Services;
using UsersManagement.TokenBase.SQL;

namespace UsersManagement.TokenBase;

public static class UsersManagementTokenBaseConfigs
{
    public static IServiceCollection 
        UsersManagementTokenBaseService(this IServiceCollection services,
        Action<UsersManagementTokenBaseOption> setupAction)
    {
        services.Configure<UsersManagementTokenBaseOption>(setupAction);
        services.AddScoped<IUserMangementService, UserMangementTokenBaseService>();
        //-----------------------------------------
        services.AddScoped<IUserRepository, SqlUserRepository>();

        return services;
    }
}
