global using UsersManagement.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UsersManagement.Common;
using UsersManagement.Options;
using UsersManagement.Repositories;
using UsersManagement.Services;
using UsersManagement.SQL;

namespace UsersManagement;


public static class UsersManagementConfigs
{
   
    //-------------------------------------------
    public static IServiceCollection AddUserManagement(this IServiceCollection services,
        Action<UserManagementOptions> options)
    {
        
        services.Configure<UserManagementOptions>(options);
        services.AddHttpContextAccessor();
        services.AddSingleton<IUserInfoService, WebUserInfoService>();
      
        services.AddScoped<IUserManagement, UserManagement>();
      
       
        
        return services;
    }
    //-------------------------------------------
    public static IServiceCollection AddUserManagementTokenBase(this IServiceCollection services,
        int ExpiresDayToken= 30,string authenticationScheme= "TokenBase",
        string tokenKey= "jwtAuthSecureKey_API_$$$", string SchemaToken= "TokenBase")
    {
        services.AddScoped<IUserRepository, SqlUserRepository>();
        services.AddScoped<ITokenRepository, SqlTokenRepository>();
        SwaggerGenConfigOptions(services);
        services.AddAuthentication($"{SchemaToken}")
                .AddJwtBearer($"{authenticationScheme}", options =>
                {
                    options.TokenValidationParameters.ValidateAudience = false;
                });
        return services;
    }
    //-------------------------------------------
    public static void UseApplicationUserManagement(this IApplicationBuilder app,bool IsDevelopment)
    {
        if (IsDevelopment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthentication();
        app.UseAuthorization();
    }
    //-------------------------------------------





    private static void SwaggerGenConfigOptions(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("SwaggerBearer", new OpenApiSecurityScheme
            {
                Scheme = "SwaggerBearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "SwaggerAuthorization",
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
                    Id = "SwaggerBearer",
                    Type = ReferenceType.SecurityScheme,
                }
            },
            new List<string>()
        }
    });
        });
    }
}
