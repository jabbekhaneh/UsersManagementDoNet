using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UsersManagement.Common;
using UsersManagement.Options;
using UsersManagement.Services;
namespace UsersManagement;


public static class UsersManagementConfigs
{

    public static IServiceCollection UserManagementTokenApiServices(this IServiceCollection services,
        Action<UserManagementOptions> options)
    {
        services.AddHttpContextAccessor();
        services.Configure(options);
        services.AddSingleton<IUserInfoService, WebUserInfoService>();
        SwaggerGenConfigOptions(services);
        JWT_TokenServiceOptions(services);

        return services;
    }
    public static void UseApplicationUserManagement(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }


    private static void JWT_TokenServiceOptions(IServiceCollection services)
    {
        services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    options.TokenValidationParameters.ValidateAudience = false;
                });
    }

    private static void SwaggerGenConfigOptions(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http,

            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme,
                }
            },
            new List<string>()
        }
    });
        });
    }
}
