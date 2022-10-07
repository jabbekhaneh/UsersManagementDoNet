global using UsersManagement.TokenBase.Common;
using Microsoft.Extensions.DependencyInjection;
using UsersManagement.TokenBase.Options;
using UsersManagement.TokenBase.Services;
using UsersManagement.TokenBase.SQL;

namespace UsersManagement.TokenBase;

public static class UsersManagementTokenBaseConfigs
{
    public static IServiceCollection
        UsersManagementTokenBaseService(this IServiceCollection services,string tokenKey,
        Action<UsersManagementTokenBaseOption> options)
    {
        services.Configure<UsersManagementTokenBaseOption>(options);
        services.AddScoped<IUserMangementService, UserMangementTokenBaseService>();
        //-----------------------------------------
        services.AddScoped<IUserRepository, SqlUserRepository>();
        services.AddSingleton<JwtAuthenticationToken>
            (new JwtAuthenticationToken(tokenKey));

        return services;
    }
}
