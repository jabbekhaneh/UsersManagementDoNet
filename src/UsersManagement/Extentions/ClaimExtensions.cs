using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersManagement.Models.DTOs;

namespace UsersManagement.Extentions;

public static class ClaimExtensions
{
    public static string GetClaim(this ClaimsPrincipal userClaimsPrincipal, string claimType)
    {
        return userClaimsPrincipal.Claims.FirstOrDefault((Claim x) => x.Type == claimType)?.Value;
    }

    public static string GenerateToken(ClaimsIdentityDto user,string key , int ExpiresDay = 1)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(key);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.NameIdentifier,user.UserId),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.GivenName),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.MobilePhone,user.Mobile),
            }),
            Expires = DateTime.UtcNow.AddDays(ExpiresDay),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                                  SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
