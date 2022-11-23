global using UsersManagement.Extentions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UsersManagement.Common;
using UsersManagement.Options;
using UsersManagement.Repositories;
using UsersManagement.Services;
using UsersManagement.SQL;

namespace UsersManagement;


public static class DependencyInjectionConfigs
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
        int? ExpiresDayToken= 30,string? authenticationScheme= "TokenBase",
        string? tokenKey= "jwtAuthSecureKey_API_$$$", string? SchemaToken= "TokenBase")
    {
        services.AddScoped<IUserRepository, SqlUserRepository>();
        services.AddScoped<ITokenRepository, SqlTokenRepository>();
        SwaggerGenConfigOptions(services);
        services.AddAuthentication($"{SchemaToken}")
                .AddJwtBearer($"{authenticationScheme}", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });
        return services;
    }
   
    //-------------------------------------------
    public static void UseApplicationUserManagementTokenBase(this IApplicationBuilder app,
                                                    bool IsDevelopment)
    {
        if (IsDevelopment)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthentication();
        app.UseAuthorization();
    }

   




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
